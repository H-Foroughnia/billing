namespace billing.Interfaces;

public interface IPasswordHelper
{
    string EncodePasswordMd5(string password);
}