using Persistence;
using BL;
using System.Text;
using System.Text.RegularExpressions;

Console.InputEncoding = System.Text.Encoding.Unicode;
Console.OutputEncoding = System.Text.Encoding.Unicode;

StaffBL staffBl = new StaffBL();
OrderBL orderBl = new OrderBL();
BookBL bookBl = new BookBL();


MainMenu();

void Line()
{
    Console.WriteLine("==============================");
}

void WaitForButton(string msg)
{
    Console.Write(msg);
    Console.ReadKey();
}

static string FormatCurrency(string currency)
{
    for (int k = currency.Length - 3; k > 0; k = k - 3)
    {
        currency = currency.Insert(k, ".");
    }
    return currency;
}

static bool IsContinue(string text)
{
    string Continue;
    bool isMatch;
    Console.Write(text);
    Continue = Console.ReadLine() ?? "";
    isMatch = Regex.IsMatch(Continue, @"^[yYnN]$");
    while (!isMatch)
    {
        Console.Write(" Chọn (Y/N)!!!: ");
        Continue = Console.ReadLine() ?? "";
        isMatch = Regex.IsMatch(Continue, @"^[yYnN]$");
    }
    if (Continue == "y" || Continue == "Y") return true;
    return false;
}

string GetPassword()
{
    StringBuilder sb = new StringBuilder();
    while (true)
    {
        ConsoleKeyInfo cki = Console.ReadKey(true);
        if (cki.Key == ConsoleKey.Enter)
        {
            Console.WriteLine();
            break;
        }
        if (cki.Key == ConsoleKey.Backspace)
        {
            if (sb.Length > 0)
            {
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write(" ");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                sb.Length--;
            }
            continue;
        }
        Console.Write('*');

        sb.Append(cki.KeyChar);
    }
    return sb.ToString();
}

int Menu(string[] menu, string name)
{
    Console.Clear();
    Line();
    Console.WriteLine(name);
    Line();
    for (int i = 0; i < menu.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {menu[i]}");
    }
    Line();
    int choice;
    do
    {
        Console.Write("Lựa chọn: ");
        int.TryParse(Console.ReadLine(), out choice);
    } while (choice < 1 || choice > menu.Length);
    return choice;
}

void MainMenu()
{
    string[] menu = { "Đăng nhập", "Thoát" };
    string name = "BOOKSTORE";
    int choice;
    while (true)
    {
        choice = Menu(menu, name);
        switch (choice)
        {
            case 1:
                Console.Write("Tên đăng nhập: ");
                string username = Console.ReadLine() ?? "";
                Console.Write("Mật khẩu: ");
                string pass = GetPassword();
                Staff staff = new Staff() { userName = username, password = pass };
                staff = staffBl.Login(staff);
                if (staff.staffRole > 0)
                {
                    MenuStore(staff);
                }
                else
                {
                    Console.WriteLine("Đăng nhập thất bại, vui lòng thử lại!");
                    WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                }
                break;

            case 2:
                if (IsContinue("Bạn có chắc là muốn thoát?(Y/N): "))
                {
                    Console.WriteLine("Đã thoát ứng dụng!");
                    Environment.Exit(0);
                }
                break;
        }
    }
}

void MenuStore(Staff staff)
{
    string[] menu = { "Tìm kiếm sách", "Tạo đơn hàng mới", "Đăng xuất" };
    string name = "CHỨC NĂNG CHÍNH";
    int choice;
    do
    {
        choice = Menu(menu, name);
        switch (choice)
        {
            case 1:
                MenuSearchBook();
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                break;

            case 2:
                CreateNewOrder(staff);
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                break;

            case 3:
                if (IsContinue("Bạn có muốn đăng xuất?(Y/N): "))
                {
                    Console.WriteLine("Đăng xuất thành công!");
                    WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                }
                else
                {
                    MenuStore(staff);
                }
                break;
        }
    } while (choice != menu.Length);
}

void MenuSearchBook()
{
    string[] menu = { "Tìm kiếm sách theo id", "Tìm kiếm sách theo tên", "Tìm kiếm sách theo danh mục", "Xem tất cả sách", "Quay lại" };
    string name = "TÌM KIẾM SẢN PHẨM";
    int choice;
    do
    {
        choice = Menu(menu, name);
        switch (choice)
        {
            case 1:
                Console.Write("Nhập id để tìm kiếm: ");
                string id = Console.ReadLine() ?? "";
                Console.WriteLine(bookBl.SearchBookByID(id));
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                break;

            case 2:
                Console.WriteLine("Gợi ý từ khoá: \"nho gay\", \"pate\", \"cho\", \"giả kim\",...");
                Console.Write("Nhập từ khoá để tìm kiếm: ");
                string nameBook = Console.ReadLine() ?? "";
                string commandTextSearchByName = $"select book.book_id, book.book_name, book.author_name, book.book_price, book.book_description, book.book_quantity, category.category_name from book inner join category on book.category_id = category.category_id where book.book_name like concat('%', {nameBook}, '%');";
                bookBl.MenuListSearchBook(commandTextSearchByName, nameBook);
                break;

            case 3:
                Console.WriteLine("Gợi ý từ khoá: \"văn học\", \"kinh tế\", \"thiếu nhi\", \"ngoại ngữ\",...");
                Console.Write("Nhập từ khoá để tìm kiếm: ");
                string nameCategory = Console.ReadLine() ?? "";
                string commandTextSearchByCategory = $"select book.book_id, book.book_name, book.author_name, book.book_price, book.book_description, book.book_quantity, category.category_name from book inner join category on book.category_id = category.category_id where category.category_name like concat('%', {nameCategory}, '%');";
                bookBl.MenuListSearchBook(commandTextSearchByCategory, nameCategory);
                break;

            case 4:
                string commandTextGetAllBook = "select book.book_id, book.book_name, book.author_name, book.book_price, book.book_description, book.book_quantity, category.category_name from book inner join category on book.category_id = category.category_id;";
                bookBl.GetAllBook(commandTextGetAllBook);
                break;

            default:
                break;
        }
    } while (choice != menu.Length);
}

void CreateNewOrder(Staff staff)
{
    Console.Clear();
    Console.WriteLine("===============================================================");
    Console.WriteLine("|                         Tạo hoá đơn                         |");
    Console.WriteLine("===============================================================");
    Orders order = new Orders();
    order.orderStaff = staff;
    do
    {
        Console.Write("Nhập ID của sách để thêm vào hoá đơn.");
        Console.Write("(Ví dụ: \"1\", \"2\", \"10\", \"21\",...): ");
        string id = Console.ReadLine() ?? "";
        Book book = bookBl.SearchBookByID(id);
        if (book.bookId == 0)
        {
            continue;
        }
        else
        {
            string strQuantity;
            bool isSuccess;
            int quantity;
            Console.Write("Nhập số lượng sách cần mua: ");
            strQuantity = Console.ReadLine() ?? "";
            isSuccess = int.TryParse(strQuantity, out quantity);
            while (!isSuccess)
            {
                Console.Write("Số lượng không hợp lệ! Nhập số lượng: ");
                strQuantity = Console.ReadLine() ?? "";
                isSuccess = int.TryParse(strQuantity, out quantity);
            }
            if (quantity <= 0)
            {
                Console.WriteLine("Thêm không thành công");
                Console.WriteLine("Quyển sách này đã hết hàng!");
                continue;
            }
            if (quantity <= 0)
            {
                Console.WriteLine("Thêm không thành công");
                Console.WriteLine("Số lượng nhập vào không hợp lệ!");
                continue;
            }
            if (quantity > book.bookQuantity)
            {
                Console.WriteLine("Thêm không thành công");
                Console.WriteLine("Số lượng mua vượt quá số lượng sách có sẵn!");
                continue;
            }
            double amount = (double)quantity * (double)book.bookPrice;
            book.bookQuantity = quantity;
            book.bookAmount = amount;
            bool add = true;
            if (order.booksList == null || order.booksList.Count == 0)
            {
                order.booksList!.Add(book);
            }
            else
            {
                for (int n = 0; n < order.booksList.Count; n++)
                {
                    if (int.Parse(id) == order.booksList[n].bookId)
                    {
                        order.booksList[n].bookQuantity += quantity;
                        order.booksList[n].bookAmount += amount;
                        add = false;
                    }
                }
                if (add) order.booksList.Add(book);
            }
        }
    } while (IsContinue("Bạn có muốn thêm sản phẩm khác vào hoá đơn không? (Y/N): "));

    if (order.booksList == null || order.booksList.Count == 0) Console.WriteLine("Hoá đơn chưa có sản phẩm!");
    if (orderBl.CreateOrder(order))
    {
        Console.WriteLine("Tạo hoá đơn thành công!");
        WaitForButton("Nhập phím bất kỳ để xem hoá đơn...");
        Console.Clear();
        string price, amount;
        Console.WriteLine("=================================================================================================");
        Console.WriteLine("|                                       Hoá đơn bán hàng                                        |");
        Console.WriteLine("-------------------------------------------------------------------------------------------------");
        Console.WriteLine($"| Thời gian: {order.orderDate,-61}    Mã hoá đơn: {order.orderId,5} |");
        Console.WriteLine($"| Nhân viên bán hàng: {order.orderStaff.staffName,-41} Địa chỉ: TP.Hà Nội                      |");
        Console.WriteLine("-------------------------------------------------------------------------------------------------");
        Console.WriteLine("| Mặt hàng                                                            Đơn giá    SL      T.Tiền |");
        foreach (Book book in order.booksList!)
        {
            price = FormatCurrency(book.bookPrice.ToString());
            amount = FormatCurrency(book.bookAmount.ToString());
            Console.WriteLine($"| {book.bookName,-65} {price,9} {book.bookQuantity,5} {amount,11} |");
            order.total += book.bookAmount;
        }
        string total = FormatCurrency(order.total.ToString());
        Console.WriteLine("-------------------------------------------------------------------------------------------------");
        Console.WriteLine($"| TỔNG TIỀN PHẢI THANH TOÁN {total,63} VND |");
        Console.WriteLine("-------------------------------------------------------------------------------------------------");
        Console.WriteLine($"| Tên khách hàng: {order.orderCustomer!.customerName,-49} Số điện thoại: {order.orderCustomer.customerPhone,12} |");
        Console.WriteLine("-------------------------------------------------------------------------------------------------");
        Console.WriteLine("|                               CẢM ƠN QUÝ KHÁCH VÀ HẸN GẶP LẠI!                                |");
        Console.WriteLine("|                                  Website: www.bookstore.com                                   |");
        Console.WriteLine("=================================================================================================");
    }
    else
    {
        Console.WriteLine("Tạo hoá đơn thất bại!");
    }
    WaitForButton("Nhập phím bất kỳ để tiếp tục...");
}


