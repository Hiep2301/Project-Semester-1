using Persistence;
using BL;
using System.Text;
using System.Text.RegularExpressions;

public class Program
{
    static StaffBL staffBl = new StaffBL();
    static OrderBL orderBl = new OrderBL();
    static BookBL bookBl = new BookBL();

    static void Main(string[] args)
    {
        MainMenu();
    }

    static void WaitForButton(string msg)
    {
        Console.Write(msg);
        Console.ReadKey();
    }

    static string FormatCurrency(string currency)
    {
        for (int i = currency.Length - 3; i > 0; i = i - 3)
        {
            currency = currency.Insert(i, ".");
        }
        return currency;
    }

    static bool IsContinue(string text)
    {
        Console.Write(text);
        string input = Console.ReadLine() ?? "";
        bool check = Regex.IsMatch(input, @"^[yYnN]$");
        while (true)
        {
            if (Regex.Match(input, @"^[yYnN]$").Success)
            {
                break;
            }
            else
            {
                Console.Write("Chọn (Y/N): ");
                input = Console.ReadLine() ?? "";
            }
        }
        if (input == "y" || input == "Y")
        {
            return true;
        }
        return false;
    }

    static string GetPassword()
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

    static int Menu(string[] menu, string name)
    {
        Console.Clear();
        Console.WriteLine("===============================================================");
        Console.WriteLine($"|                         {name,-25}           |");
        Console.WriteLine("===============================================================");
        for (int i = 0; i < menu.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {menu[i]}");
        }
        Console.WriteLine("===============================================================");
        int choice;
        do
        {
            Console.Write("Chọn: ");
            int.TryParse(Console.ReadLine(), out choice);
        } while (choice < 1 || choice > menu.Length);
        return choice;
    }

    static void MainMenu()
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
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("===============================================================");
                        Console.WriteLine("|                          Đăng nhập                          |");
                        Console.WriteLine("===============================================================");
                        Console.Write("Tên đăng nhập: ");
                        string username = Console.ReadLine() ?? "";
                        Console.Write("Mật khẩu: ");
                        string pass = GetPassword();
                        Staff staff = new Staff() { userName = username, password = pass };
                        staff = staffBl.Login(staff);
                        if (staff.staffRole > 0)
                        {
                            MenuStore(staff);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Đăng nhập thất bại, vui lòng thử lại!");
                            WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        }
                    }
                    break;

                case 2:
                    if (IsContinue("Bạn có chắc là muốn thoát? (Y/N): "))
                    {
                        Console.WriteLine("Đã thoát ứng dụng!");
                        Environment.Exit(0);
                    }
                    break;
            }
        }
    }

    static void MenuStore(Staff staff)
    {
        string[] menu = { "Tìm kiếm sách", "Tạo đơn hàng mới", "Lịch sử giao dịch", "Đăng xuất" };
        string name = "CHỨC NĂNG CHÍNH";
        int choice;
        do
        {
            choice = Menu(menu, name);
            switch (choice)
            {
                case 1:
                    MenuSearchBook();
                    break;

                case 2:
                    CreateNewOrder(staff);
                    break;

                case 3:
                    Console.Clear();
                    List<Orders> orderList = new List<Orders>();
                    orderList = orderBl.GetAllOrderInDay();
                    if (orderList == null || orderList.Count == 0)
                    {
                        Console.Clear();
                        WaitForButton("Chưa có hóa đơn được tạo trong ngày hôm nay! Nhập phím bất kỳ để quay lại...");
                        break;
                    }
                    Console.WriteLine("=========================================================================================================");
                    Console.WriteLine("|                                           Lịch sử giao dịch                                           |");
                    Console.WriteLine("=========================================================================================================");
                    Console.WriteLine("| Mã hóa đơn   Người tạo             Khách hàng             Thời gian tạo             Tổng tiền         |");
                    Console.WriteLine("| ----------   --------------        --------------         -------------             ---------         |");
                    foreach (var item in orderBl.GetAllOrderInDay())
                    {
                        Console.WriteLine($"| {item.orderId,-12} {item.staffName,-21} {item.customerName,-22} {item.orderDate,-25} {FormatCurrency(item.total.ToString()),-17} |");

                    }
                    Console.WriteLine("=================================================================================");
                    WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                    break;

                case 4:
                    if (IsContinue("Bạn có muốn đăng xuất? (Y/N): "))
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

    static void MenuSearchBook()
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
                    Console.Clear();
                    Console.WriteLine("=================================================================================");
                    Console.WriteLine("|                             Tìm kiếm sách theo id                             |");
                    Console.WriteLine("=================================================================================");
                    Console.WriteLine("Gợi ý từ khoá: \"1\", \"2\", \"3\", \"4\",...");
                    Console.Write("Nhập id để tìm kiếm: ");
                    string id = Console.ReadLine() ?? "";
                    bookBl.SearchBookByID(id);
                    WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("=================================================================================");
                    Console.WriteLine("|                             Tìm kiếm sách theo tên                            |");
                    Console.WriteLine("=================================================================================");
                    Console.WriteLine("Gợi ý từ khoá: \"tiếng anh\", \"tài chính\", \"sức khỏe\", \"giả kim\",...");
                    Console.Write("Nhập từ khoá để tìm kiếm: ");
                    string nameBook = Console.ReadLine() ?? "";
                    string commandTextSearchByName = $"select book.book_id, book.book_name, book.author_name, book.book_price, book.book_description, book.book_quantity, category.category_name from book inner join category on book.category_id = category.category_id where book.book_name like concat('%', '{nameBook}', '%');";
                    bookBl.MenuListSearchBook(commandTextSearchByName, nameBook);
                    WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("=================================================================================");
                    Console.WriteLine("|                          Tìm kiếm sách theo danh mục                          |");
                    Console.WriteLine("=================================================================================");
                    Console.WriteLine("Gợi ý từ khoá: \"văn học\", \"kinh tế\", \"thiếu nhi\", \"ngoại ngữ\",...");
                    Console.Write("Nhập từ khoá để tìm kiếm: ");
                    string nameCategory = Console.ReadLine() ?? "";
                    string commandTextSearchByCategory = $"select book.book_id, book.book_name, book.author_name, book.book_price, book.book_description, book.book_quantity, category.category_name from book inner join category on book.category_id = category.category_id where category.category_name like concat('%', '{nameCategory}', '%');";
                    bookBl.MenuListSearchBook(commandTextSearchByCategory, nameCategory);
                    WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                    break;

                case 4:
                    string commandTextGetAllBook = "select book.book_id, book.book_name, book.author_name, book.book_price, book.book_description, book.book_quantity, category.category_name from book inner join category on book.category_id = category.category_id;";
                    bookBl.GetAllBook(commandTextGetAllBook);
                    WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                    break;

                default:
                    break;
            }
        } while (choice != menu.Length);
    }

    static void CreateNewOrder(Staff staff)
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
            if (book.bookId <= 0)
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
                if (book.bookQuantity <= 0)
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
                decimal amount = (decimal)quantity * book.bookPrice;
                book.bookQuantity = quantity;
                book.bookAmount = amount;
                bool add = true;
                if (order.booksList == null || order.booksList.Count == 0)
                {
                    order.booksList!.Add(book);
                }
                else
                {
                    for (int i = 0; i < order.booksList.Count; i++)
                    {
                        if (int.Parse(id) == order.booksList[i].bookId)
                        {
                            order.booksList[i].bookQuantity += quantity;
                            order.booksList[i].bookAmount += amount;
                            add = false;
                        }
                    }
                    if (add)
                    {
                        order.booksList.Add(book);
                    }
                }
            }
        } while (IsContinue("Bạn có muốn thêm sản phẩm khác vào hoá đơn không? (Y/N): "));

        if (orderBl.CreateOrder(order))
        {
            Console.WriteLine("Tạo hoá đơn thành công!");
            WaitForButton("Nhập phím bất kỳ để xem hoá đơn...");
            string price, amount;
            Console.Clear();
            Console.WriteLine("=================================================================================================");
            Console.WriteLine("|                                       Hoá đơn bán hàng                                        |");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Thời gian: {order.orderDate,-61}   Mã hoá đơn: {order.orderId,6} |");
            Console.WriteLine($"| Nhân viên bán hàng: {order.orderStaff.staffName,-41}              Địa chỉ: TP.Hà Nội |");
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
            Payment payment = new Payment();
            string paymentAmount;
            string refund;
            while (true)
            {
                Console.Write("Nhập số tiền khách thanh toán: ");
                paymentAmount = Console.ReadLine() ?? "";
                while (true)
                {
                    if (Regex.Match(paymentAmount, @"((?<=\s)|^)[-+]?((\d{1,3}([,\s.']\d{3})*)|\d+)([.,/-]\d+)?((?=\s)|$)").Success)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Số tiền không hợp lệ!");
                        Console.Write("Nhập số tiền khách thanh toán: ");
                        paymentAmount = Console.ReadLine() ?? "";
                    }
                }
                if (Convert.ToDecimal(paymentAmount) >= order.total)
                {
                    payment.paymentAmount = Convert.ToDecimal(paymentAmount);
                    paymentAmount = FormatCurrency(payment.paymentAmount.ToString());

                    payment.refund = payment.paymentAmount - order.total;
                    refund = FormatCurrency(payment.refund.ToString());
                    break;
                }
                else
                {
                    Console.WriteLine("Số tiền bạn nhập không đúng! Vui lòng nhập lại!");
                }
            }
            Console.Clear();
            Console.WriteLine("=================================================================================================");
            Console.WriteLine("|                                       Hoá đơn bán hàng                                        |");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Thời gian: {order.orderDate,-61}   Mã hoá đơn: {order.orderId,6} |");
            Console.WriteLine($"| Nhân viên bán hàng: {order.orderStaff.staffName,-41}              Địa chỉ: TP.Hà Nội |");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine("| Mặt hàng                                                            Đơn giá    SL      T.Tiền |");
            foreach (Book book in order.booksList!)
            {
                price = FormatCurrency(book.bookPrice.ToString());
                amount = FormatCurrency(book.bookAmount.ToString());
                Console.WriteLine($"| {book.bookName,-65} {price,9} {book.bookQuantity,5} {amount,11} |");
                order.total += book.bookAmount;
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine($"|                                                        + Tổng tiền      : {total,15} VND |");
            Console.WriteLine($"|                                                        + Tiền thanh toán: {paymentAmount,15} VND |");
            Console.WriteLine($"|                                                        + Hoàn tiền      : {refund,15} VND |");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine("|                               CẢM ƠN QUÝ KHÁCH VÀ HẸN GẶP LẠI!                                |");
            Console.WriteLine("|                                  Website: www.bookstore.com                                   |");
            Console.WriteLine("=================================================================================================");

        }
        else
        {
            Console.WriteLine("Hoá đơn chưa có sản phẩm!");
            Console.WriteLine("Tạo hoá đơn thất bại!");
        }
        WaitForButton("Nhập phím bất kỳ để tiếp tục...");
    }
}




