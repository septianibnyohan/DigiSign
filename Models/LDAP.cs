namespace DigiSign.Models
{
    public class LDAP
    {
        public bool status { get; set; }
        public string displayname { get; set; }
        public string samaccountname { get; set; }
        public string name { get; set; }
        public string[] memberof { get; set; }
        public string userprincipalname { get; set; }
        public string givenname { get; set; }
        public string description { get; set; }
        public string message { get; set; }
    }
}