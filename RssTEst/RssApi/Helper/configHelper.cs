namespace RssApi.Helper
{
    public static class ConfigHelper
    {
        public static int NumberLast
        {
            get
            {
                var valeur = System.Configuration.ConfigurationManager.AppSettings["numberLast"];
                int retour;
                if (int.TryParse(valeur, out retour))
                {
                    return retour;
                }
                return 30;
            }
        }
    }
}