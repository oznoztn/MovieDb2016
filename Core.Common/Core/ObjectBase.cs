using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Runtime.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace Core.Common.Core
{
    [DataContract(Namespace = "MovieDb")]
    public abstract class ObjectBase : IExtensibleDataObject
    {
        public ObjectBase()
        {
            // _Validator : bu sözkonusu entity'nin validation class'ının bir instance'ı. 
            // Diğer bir ifadeyle entity'nin (misal Car) içinde tanımlanan override metodundan dönen sonuç bu (misal CarValidator)

            _Validator = GetValidator();
            //Validate();
        }

        public ExtensionDataObject ExtensionData { get; set; }
        protected IValidator _Validator = null;
        protected IEnumerable<ValidationFailure> _ValidationErrors = null;
        public static CompositionContainer Container { get; set; }
        
        #region Validation

        protected virtual IValidator GetValidator()
        {
            return null;
        }

        public IEnumerable<ValidationFailure> ValidationErrors
        {
            get { return _ValidationErrors; }
        }

        public void Validate()
        {
            if (_Validator != null)
            {
                ValidationResult results = _Validator.Validate(this);
                _ValidationErrors = results.Errors;
            }
        }

        // Sadece BİR property'nin valide edildiği durumlar için
        public virtual bool IsValid
        {
            get
            {
                if (_ValidationErrors != null && _ValidationErrors.Any())
                    return false;
                else
                    return true;
            }
        }
        #endregion
    }
}
