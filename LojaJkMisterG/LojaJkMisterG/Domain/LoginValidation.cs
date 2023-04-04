using System.ComponentModel.DataAnnotations;

namespace LojaJkMisterG.Domain
{
    public class LoginValidation : ValidationAttribute
    {
        public LoginValidation() { }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;
            bool valido = ValidaLetraLogin(value.ToString());
            return valido;
        }

        public bool ValidaLetraLogin(string login)
        {
            if (login.Trim().ToLower().Contains('.'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
