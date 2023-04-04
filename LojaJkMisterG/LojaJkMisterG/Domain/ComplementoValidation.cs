using System.ComponentModel.DataAnnotations;

namespace LojaJkMisterG.Domain
{
    public class ComplementoValidation : ValidationAttribute
    {
        public ComplementoValidation()
        {

        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;
            bool valido = VerificaComplemento(value.ToString());
            return valido;
        }

        public bool VerificaComplemento(string complemento)
        {
            if (complemento == "Casa" || complemento == "casa")
            {
                return true;
            }
            if (complemento == "Apartamento" || complemento == "apartamento")
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
