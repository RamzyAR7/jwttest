namespace Test.Entities
{
    public class Jwt
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Lifetime { get; set; }
        public string SiningKey { get; set; }
    }
}
