namespace ClientsAPI.Models
{
    /// <summary>
    /// Класс для определения некорректного ввода при добавлении нового пользователя
    /// </summary>
    public class Response
    {
        public bool IsValid { get; set; } = false;
        public string Value { get; set; } = null!;

        public Response(string value, bool isValid = false)
        {
            Value = value;
            IsValid = isValid;
        }
    }
}
