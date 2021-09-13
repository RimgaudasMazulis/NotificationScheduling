namespace NotificationScheduling.Domain.Exceptions
{
    public static class ErrorCodes
    {
        public const string NoPermission = "NoPermissionError";
        public const string NotExists = "ReceiptNotExistsError";
        public const string WrongStatus = "WrongStatusError";

        public const string NullRef = "NullRefError";
        public const string InternalServer = "InternalServerError";
        public const string Other = "OtherError";
    }
}
