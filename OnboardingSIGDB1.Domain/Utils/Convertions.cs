using System;

namespace OnboardingSIGDB1.Domain.Utils
{
    public static class Convertions
    {
        public static string GetCnpjSemMascara(string cnpj) {
            return cnpj.Replace("-", "").Replace(".","").Replace("/","");
        }

        public static string GetCnpjComMascara(string cnpj)
        {
            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }
        
        public static string GetCpfSemMascara(string cpf) {
            return cpf.Replace("-", "").Replace(".","");
        }

        public static string GetCpfComMascara(string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }


        public static DateTime? GetDateTime(string date)
        {
            try
            {
                var dateResult = DateTime.Parse(date);
                return dateResult;
            }
            catch
            {
                return null;
            }
        }

        public static string GetDateFormatted(DateTime? date)
        {
            return date.HasValue ? date.Value.ToString("dd/MM/yyyy") : "";
        }
    }
}