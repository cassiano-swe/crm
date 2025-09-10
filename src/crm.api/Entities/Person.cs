namespace crm.api.Entities;

public class Person(string name)
{
    public int Id { get; init; }
    public string Name { get; init; } = name;
    public string? Cpf { get; init; }
    public Organization? Organization { get; init; }
    public string? JobTitle { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? Description { get; init; }
    public Category? Category { get; init; }
    public LeadOrigin? LeadOrigin { get; init; }
    public PersonContact? Contact { get; set; }

    public static Person CreatePersonWithContact(string name,
        string? cpf,
        Organization? organization,
        string? jobTitle,
        DateTime? birthDate,
        string? description,
        Category? category,
        LeadOrigin? leadOrigin,
        string? availablePhone,
        string? email,
        string? facebook,
        string? faxPhone,
        string? instagram,
        string? linkedin,
        string? mobilePhone,
        int? phoneExtension,
        string? skype,
        string? twitter,
        string? whatsapp,
        string? workPhone) =>
        new Person(name: name)
        {
            Cpf = cpf,
            Organization = organization,
            JobTitle = jobTitle,
            BirthDate = birthDate,
            Description = description,
            Category = category,
            LeadOrigin = leadOrigin,
            Contact = PersonContact.Create(
                availablePhone: availablePhone,
                email: email,
                facebook: facebook,
                faxPhone: faxPhone,
                instagram: instagram,
                linkedin: linkedin,
                mobilePhone: mobilePhone,
                phoneExtension: phoneExtension,
                skype: skype,
                twitter: twitter,
                whatsapp: whatsapp,
                workPhone: workPhone)
        };

    public static Person CreatePersonWithContact(int Id,
            string name,
            string? cpf,
            Organization? organization,
            string? jobTitle,
            DateTime? birthDate,
            string? description,
            Category? category,
            LeadOrigin? leadOrigin,
            string? availablePhone,
            string? email,
            string? facebook,
            string? faxPhone,
            string? instagram,
            string? linkedin,
            string? mobilePhone,
            int? phoneExtension,
            string? skype,
            string? twitter,
            string? whatsapp,
            string? workPhone) =>
            new Person(name: name)
            {
                Id = Id,
                Cpf = cpf,
                Organization = organization,
                JobTitle = jobTitle,
                BirthDate = birthDate,
                Description = description,
                Category = category,
                LeadOrigin = leadOrigin,
                Contact = PersonContact.Create(
                    availablePhone: availablePhone,
                    email: email,
                    facebook: facebook,
                    faxPhone: faxPhone,
                    instagram: instagram,
                    linkedin: linkedin,
                    mobilePhone: mobilePhone,
                    phoneExtension: phoneExtension,
                    skype: skype,
                    twitter: twitter,
                    whatsapp: whatsapp,
                    workPhone: workPhone)
            };

    public void CreateContact(string? availablePhone,
            string? email,
            string? facebook,
            string? faxPhone,
            string? instagram,
            string? linkedin,
            string? mobilePhone,
            int? phoneExtension,
            string? skype,
            string? twitter,
            string? whatsapp,
            string? workPhone)
    {
        Contact = PersonContact.Create(
                    availablePhone: availablePhone,
                    email: email,
                    facebook: facebook,
                    faxPhone: faxPhone,
                    instagram: instagram,
                    linkedin: linkedin,
                    mobilePhone: mobilePhone,
                    phoneExtension: phoneExtension,
                    skype: skype,
                    twitter: twitter,
                    whatsapp: whatsapp,
                    workPhone: workPhone);
    }

    public class PersonContact
    {
        private PersonContact()
        {
        }

        public int Id { get; init; }
        public int PeopleId { get; init; }
        public string? AvailablePhone { get; init; }
        public string? Email { get; init; }
        public string? Facebook { get; init; }
        public string? FaxPhone { get; init; }
        public string? Instagram { get; init; }
        public string? Linkedin { get; init; }
        public string? MobilePhone { get; init; }
        public int? PhoneExtension { get; init; }
        public string? Skype { get; init; }
        public string? Twitter { get; init; }
        public string? Whatsapp { get; init; }
        public string? WorkPhone { get; init; }

        internal static PersonContact Create(
            string? availablePhone,
            string? email,
            string? facebook,
            string? faxPhone,
            string? instagram,
            string? linkedin,
            string? mobilePhone,
            int? phoneExtension,
            string? skype,
            string? twitter,
            string? whatsapp,
            string? workPhone) =>
            new PersonContact
            {
                AvailablePhone = availablePhone,
                Email = email,
                Facebook = facebook,
                FaxPhone = faxPhone,
                Instagram = instagram,
                Linkedin = linkedin,
                MobilePhone = mobilePhone,
                PhoneExtension = phoneExtension,
                Skype = skype,
                Twitter = twitter,
                Whatsapp = whatsapp,
                WorkPhone = workPhone
            };
    }
}
