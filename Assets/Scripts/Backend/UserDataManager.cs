public static class UserDataManager
{
    public static class UserData
    {
        public static string _userId;
        public static string _email;
        public static string _nickname;
    }

    public static void Logout()
    {
        UserData._userId = string.Empty;
        UserData._email = string.Empty;
        UserData._nickname = string.Empty;
    }
}
