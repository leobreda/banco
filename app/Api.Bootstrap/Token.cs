namespace Api.Bootstrap
{
    public class Token
    {
        public static string Get(uint agencia, uint conta, uint dac, uint minutes)
        {
            string str = $"{DateTime.Now.ToString("yyyyMMddHHmmssffff")}|{agencia.ToString()}|{conta.ToString()}|{dac.ToString()}";
            return Strings.Zip(str);
        }
        public static bool IsValid(string token, uint agencia, uint conta, uint dac, uint minutes)
        {
            string str = Strings.Unzip(token);

            string[] arr = str.Split('|');
            try
            {
                DateTime dt = DateTime.ParseExact(arr[0], "yyyyMMddHHmmssffff", null);
                DateTime now = DateTime.Now;
                if ((DateTime.Now - dt).TotalMinutes > minutes)
                    return false;

                if (arr[1] != agencia.ToString()) return false;
                if (arr[2] != conta.ToString()) return false;
                if (arr[3] != dac.ToString()) return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
