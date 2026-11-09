namespace UserService.Exceptions;

public class UnauthorizedException(string message) : Exception(message);