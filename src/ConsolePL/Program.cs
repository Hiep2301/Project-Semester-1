using Persistence;

User admin = new Admin("admin", "admin", "Hiep", "Ngo", "123456789");
User customer = new Customer("huy123", "huy123", "Huy", "Nguyen", "123456789", "Hà Nội");

Console.WriteLine($"{admin}");
Console.WriteLine($"{customer}");
