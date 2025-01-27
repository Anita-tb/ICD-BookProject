using ICD.Framework.Model;

namespace ICD.BookProject;

public class CreateTokenQuery : Query
{
    public string Username { get; set; }
    public string SecretKey { get; set; } = "zS7ca8sN5tS2kNOLi5B7Hz0xxgcSNaXZ";
    public int ExpireAfterHours { get; set; } = 12;
    public string Issuer { get; set; } = "http://usm.icdgroup.org";
}