using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities.DTOs
{
    [DataContract]
    public class ActorCreationData : ObjectBase
    {
        class ActorCreationDataValidator : AbstractValidator<ActorCreationData>
        {
            public ActorCreationDataValidator()
            {
                
            }
        }

        protected override IValidator GetValidator()
        {
            return new ActorCreationDataValidator();
        }

        private string _fullName;
        private int _countryId = 1; // 1 for default value: "undefined"
        private int _stateId = 1; // 1 for default value: "undefined"
        private int _countyId = 1; // 1 for default value: "undefined"
        private DateTime _createdAt;

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string FullName {
            get
            {
                if (_fullName == null)
                    return string.Format("{0} {1}", FirstName, LastName);

                return _fullName;
            }
            set
            {
                if (value != null)
                {
                    _fullName = value;
                }
                else
                {
                    value = string.Format("{0} {1}", FirstName, LastName);
                    _fullName = value;
                }
            }
        }

        [DataMember]
        public int CountryId
        {
            get
            {
                return _countryId;
            }
            set
            {
                _countryId = value;
            }
        }

        [DataMember]
        public int StateId
        {
            get
            {
                return _stateId;
            }
            set
            {
                _stateId = value;
            }
        }

        [DataMember]
        public int CountyId
        {
            get
            {
                return _countyId;
            }
            set
            {
                _countyId = value;
            }
        }

        [DataMember]
        public DateTime CreatedAt
        {
            get
            {
                // Burası EDIT ve CREATE işlemlerinde işe yarıyor.

                // Create işleminde, DateTime.Now invalid olmakta ... (0001)
                
                // Yani Edit işleminde veritabanında bulunan değeri tekrar editlenen entity'ye veriyoruz.
                // _createdAt değeri @Html.HiddenFor(t => t.CreatedAt)'den geliyor.
                return _createdAt.Year == 0001 ? DateTime.Now : _createdAt;
            }
            set
            {
                _createdAt = value;
            }
        }
        
        [DataMember]
        public bool Gender { get; set; }

        [DataMember]
        public string Biography { get; set; }

        [DataMember]
        public string ImdbLink { get; set; }

        //[DataMember]
        //public int CountryId { get; set; }

        //[DataMember]
        //public int StateId { get; set; }

        //[DataMember]
        //public int CountyId { get; set; }

        [DataMember]
        public int[] MovieIds { get; set; }

        [DataMember]
        public string Photo { get; set; }

        //[DataMember]
        //public DateTime CreatedAt { get; set; }

        [DataMember]
        public DateTime? BirthDate { get; set; }

        [DataMember]
        public DateTime? DeathDate { get; set; }
    }
}
