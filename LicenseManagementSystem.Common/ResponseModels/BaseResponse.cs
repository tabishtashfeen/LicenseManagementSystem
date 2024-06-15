namespace LicenseManagementSystem.Common.ResponseModels
{
    public class BaseResponse
    {
        public bool Success { get; set; } = true;
        public object Result { get; set; }
        public object Errors { get; set; }
        public string Message { get; set; }
    }
}
