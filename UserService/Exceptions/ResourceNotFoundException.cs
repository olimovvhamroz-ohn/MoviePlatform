namespace UserService.Exceptions;

public class ResourceNotFoundException(string message) : System.Exception(message);