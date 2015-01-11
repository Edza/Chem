using System.Collections.Generic;

namespace Chem.Controllers.Utility
{
    public static class Local
    {
        private static bool isCurrEng = true;

        private static Dictionary<string, string> EngDict = new Dictionary<string, string>()
        {
            {"Home", "Home"},
            {"Reagents", "Reagents"},
            {"Reactions", "Reactions"},
            {"Movies", "Movies"},
            {"LogOff", "LogOff"},
            {"Log in", "Log in"},
            {"Register", "Register"},
            {"SearchByHeading", "Search by heading:"},
            {"SearchByReagents", "Search by reagents:"},
            {"SearchQuery", "You searched:"},
            {"NewVideos", "New videos:"},
            {"Index", "Index"},
            {"Edit", "Edit"},
            {"DeleteUser", "Delete user"},
            {"CreateNew", "Create new"},
        };

        private static Dictionary<string, string> LatDict = new Dictionary<string, string>()
        {
            {"Home", "Sākums"},
            {"Reagents", "Reaģenti"},
            {"Reactions", "Reakcijas"},
            {"Movies", "Video"},
            {"LogOff", "Atteikties"},
            {"Log in", "Autorizēties"},
            {"Register", "Pieteikties"},
            {"SearchByHeading", "Meklēšana pēc virsraksta:"},
            {"SearchByReagents", "Meklēšana ar reaģentu sarakstu:"},
            {"SearchQuery", "Tika meklēts:"},
            {"NewVideos", "Jaunākie video:"},
            {"Index", "Saraksts"},
            {"Edit", "Labot"},
            {"DeleteUser", "Dzēst lietotaju"},
            {"CreateNew", "Izveidot jaunu"},
        };

        public static string GetInCurrentLang(string key)
        {
            if (isCurrEng)
                return EngDict[key];
            else
                return LatDict[key];
        }

        public static void SetCurrLang(bool isEnglish)
        {
            isCurrEng = isEnglish;
        }
    }
}