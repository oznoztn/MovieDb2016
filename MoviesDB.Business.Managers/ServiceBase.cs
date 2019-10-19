using Core.Common.Contracts;
using Core.Common.Core;
using MoviesDB.Business.Entities;
using MoviesDb.Common;
using System;
using System.ComponentModel.Composition;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading;

namespace MoviesDB.Business.Services
{
    public class ServiceBase
    {
        User _AuthorizationAccount = null;
        string LoginName = string.Empty;

        public ServiceBase()
        {
            OperationContext context = OperationContext.Current;
            if (context != null)
            {
                try
                {
                    // Not aşağıda (incoming message header)
                    // Client tarafından gelen OutgoingMessageHeaders'a IncomingMessageHeaders ile ulaşıyoruz.
                    LoginName = context.IncomingMessageHeaders.GetHeader<string>("String", "System");
                    if (LoginName.IndexOf(@"\") > 1)
                        LoginName = string.Empty;
                }
                catch
                {
                    LoginName = string.Empty;
                }
            }

            if (!string.IsNullOrWhiteSpace(LoginName))
                _AuthorizationAccount = LoadAuthorizationValidationAccount(LoginName);

            // Dependency Resolving
            if (ObjectBase.Container != null)
                ObjectBase.Container.SatisfyImportsOnce(this);
        }
        // BU OVERRIDE EDİLMELİ
        protected virtual User LoadAuthorizationValidationAccount(string loginName)
        {
            return null;
        }

        protected void ValidateAuthorization(ILoggedUser entity)
        {
            // admin rolünde ise kontrolü atlıyoruz. -> Admin rolünde değil ise gir
            // Servis operasyonlarının (yada metotlarının) güvenliği için kullanılan PrincipalPermission attribute'u 
            // burada yaptığımızdan farklı bir şey yapmıyor: O da Thread.CurrentPrincipal.IsInRole("HerhangiBirRol") kullanıyor
            if (!Thread.CurrentPrincipal.IsInRole(Roles.Admin))
            {
                if (_AuthorizationAccount != null)
                {
                    if (LoginName != string.Empty && entity.LoggedAccountId != _AuthorizationAccount.Id)
                    {
                        /*
                         * Servis tarafında exception ile uğraşırken
                         * Önce fırlatılacak (lafın gelişi, normalde servis tarafında bir şey fırlatılmaz. Diğer nota bak) exception'ın
                         * örneği oluşturulur. Daha sonra FaultException<T> kullanılır. T Burada kullanılacak olan exception...
                         */
                        AuthorizationValidationException ex = 
                            new AuthorizationValidationException("Attempt to access a secure record with improper user authorization validation.");
                        throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
                    }
                }
            }
        }

        // T : Piece of code
        // Metodun alacağı argüman, bir kod snippet'i olacağından (bir kod parçası (try bloğu içindeki kodları)), Func<T> kullanıldı.       
        protected T ExecuteFaultHandledOperation<T>(Func<T> codetoExecute)
        {
            try
            {
                // gelen kodu (codetoExecute) çalıştırmak ve T tipinde bir type döndermek için Invoke() metodu...
                return codetoExecute.Invoke();
            }
            catch (AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        protected void ExecuteFaultHandledOperation(Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch (AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
/*              ACCESSING TO INCOMING HEAD MESSAGE
 * 
 * SOAP header string tipinde log-in olmuş kullanıcının e-mail adresini taşıyor.
 * SOAP header' burada OperationContext ile erişip bu bilgiyi çekip alacağız.
 * 
 * Yaptığımız şey servisteki metotlardan birini çağıran logged user'ın email adresini almak.
 * Örneğin UserListService'indeki GetUserLists(string loginEmail) metodunu biri çağırmış olsun
 * Bu metodu çağıran kişi ile 
 * 
 */
 
/*
 * 
 *  protected T ExecuteFaultHandledOperation<T>(Func<T> codetoExecute)
 *  protected void ExecuteFaultHandledOperation(Action codetoExecute)
 * 
 *  Biri T dönderiyor; Diğeri ise void.
 */

/*
     * IncomingMessageHeaders her servis çağrısında bulunan bir değişkendir
     * Current SOAP header'a ulaşmamıza olanak verir.
     * HttpContext.Current gibi 
     * Instantiate etmeden kullanabildiğimiz Request ve Session variable gibi
     * WCF deki OperationContext de HttpContext gibidir, tek fark örneklenmesi gerekir.
*/