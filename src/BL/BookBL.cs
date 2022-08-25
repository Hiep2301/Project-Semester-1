using Persistence;
using DAL;
using System.Text.RegularExpressions;

namespace BL
{
    public class BookBL
    {
        private BookDAL bookDal = new BookDAL();

        public Book SearchBookByID(string searchKeyWord)
        {
            Book book = new Book();
            book = bookDal.GetBookById(searchKeyWord, book);
            string search = '"' + searchKeyWord + '"';
            if (book.bookId <= 0)
            {
                Console.WriteLine($"Không tồn tại sản phẩm phù hợp với từ khoá là {search}");
            }
            else
            {
                ShowBookDetail(book, searchKeyWord);
            }
            return book;
        }

        public void GetAllBook(string commandText)
        {
            List<Book> list = new List<Book>();
            list = bookDal.GetBookList(list, commandText);
            if (list.Count == 0)
            {
                Console.WriteLine("Không có sản phẩm!");
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            else
            {
                int size = 10;
                int page = 1;
                int pages = (int)Math.Ceiling((double)list.Count / size);
                int i, k = 0;
                string choice, price;
                for (; ; )
                {
                    Console.Clear();
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine("|                                                       Tất cả sách                                                            |");
                    Console.WriteLine($"|                                                                                                                    Trang {page}/{pages} |");
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine("| Mã Sách | Tên sách                                                            | Giá           | Loại                         |");
                    if (list.Count < size)
                    {
                        for (i = 0; i < list.Count; i++)
                        {
                            price = FormatCurrency(list[i].bookPrice.ToString());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"| {list[i].bookId,-7} | {list[i].bookName,-67} | {price,-13} | {list[i].bookCategory,-28} |");
                        }
                    }
                    else
                    {
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            if (i == list.Count) break;
                            price = FormatCurrency(list[i].bookPrice.ToString());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"| {list[i].bookId,-7} | {list[i].bookName,-67} | {price,-13} | {list[i].bookCategory,-28} |");
                        }
                    }
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine("Nhập P để xem trang trước.");
                    Console.WriteLine("Nhập N để xem trang sau.");
                    Console.WriteLine("Nhập P kèm số trang để xem trang mong muốn (VD: P1, P2,...).");
                    Console.WriteLine("Nhập ID để xem chi tiết thông tin sách.");
                    Console.WriteLine("Nhập 0 để quay lại.");
                    Console.WriteLine("-----------------------------------------------------------------");
                    Console.Write("Chọn: ");
                    choice = Console.ReadLine() ?? "";
                    while (!(Regex.IsMatch(choice, @"([PpNn]|[1-9]|^0$)")))
                    {
                        Console.Write("Lựa chọn không hợp lệ! Chọn lại: ");
                        choice = Console.ReadLine() ?? "";
                    }
                    choice = choice.Trim();
                    choice = choice.ToLower();
                    string number = Regex.Match(choice, @"\d+").Value;
                    string pageNum = "p" + number;
                    if (choice == "n")
                    {
                        if (page == pages)
                        {
                            WaitForButton("Không có trang sau! Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = page + 1;
                            k = k + 10;
                        }
                    }
                    else if (choice == "p")
                    {
                        if (page == 1)
                        {
                            WaitForButton("Không có trang trước! Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = page - 1;
                            k = k - 10;
                        }
                    }
                    else if (choice == pageNum)
                    {
                        if (int.Parse(number) < 0 || int.Parse(number) > pages || int.Parse(number) == 0)
                        {
                            Console.WriteLine($"Không tồn tại trang {int.Parse(number)}");
                            WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = int.Parse(number);
                            k = (int.Parse(number) - 1) * 10;
                        }
                    }
                    else if (choice == "0")
                    {
                        return;
                    }
                    else
                    {
                        bool found = false;
                        string search1 = '"' + choice + '"';
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            try
                            {
                                if (int.Parse(choice) == list[i].bookId)
                                {
                                    ShowBookDetail(list[i], search1);
                                    WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                                    found = true;
                                    break;
                                }
                            }
                            catch (System.FormatException) { }
                            catch (System.ArgumentOutOfRangeException) { }
                        }
                        if (!(found))
                        {
                            Console.WriteLine("ID không phù hợp!");
                            WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        }
                    }
                }
            }
        }

        public void MenuListSearchBook(string commandText, string searchKeyWord)
        {
            List<Book> list = new List<Book>();
            list = bookDal.GetBookList(list, commandText);
            string search = '"' + searchKeyWord + '"';
            if (list.Count == 0)
            {
                Console.WriteLine($"Không tồn tại sản phẩm phù hợp với từ khoá là '{search}'");
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            else
            {
                int size = 10;
                int page = 1;
                int pages = (int)Math.Ceiling((double)list.Count / size);
                int i, k = 0;
                string choice, price;
                for (; ; )
                {
                    Console.Clear();
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine($"|                                            Kết quả tìm kiếm với từ khoá là {search,-49} |");
                    Console.WriteLine($"| Tìm thấy khoảng {list.Count} sách                                                                                             Trang {page}/{pages} |");
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine("| Mã Sách | Tên sách                                                            | Giá           | Loại                         |");
                    if (list.Count < size)
                    {
                        for (i = 0; i < list.Count; i++)
                        {
                            price = FormatCurrency(list[i].bookPrice.ToString());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"| {list[i].bookId,-7} | {list[i].bookName,-67} | {price,-13} | {list[i].bookCategory,-28} |");
                        }
                    }
                    else
                    {
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            if (i == list.Count) break;
                            price = FormatCurrency(list[i].bookPrice.ToString());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"| {list[i].bookId,-7} | {list[i].bookName,-67} | {price,-13} | {list[i].bookCategory,-28} |");
                        }
                    }
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine("Nhập P để xem trang trước.");
                    Console.WriteLine("Nhập N để xem trang sau.");
                    Console.WriteLine("Nhập P kèm số trang để xem trang mong muốn (VD: P1, P2,...).");
                    Console.WriteLine("Nhập ID để xem chi tiết thông tin sách.");
                    Console.WriteLine("Nhập 0 để quay lại.");
                    Console.WriteLine("-----------------------------------------------------------------");
                    Console.Write("Chọn: ");
                    choice = Console.ReadLine() ?? "";
                    while (!(Regex.IsMatch(choice, @"([PpNn]|[1-9]|^0$)")))
                    {
                        Console.Write("Lựa chọn không hợp lệ! Chọn lại: ");
                        choice = Console.ReadLine() ?? "";
                    }
                    choice = choice.Trim();
                    choice = choice.ToLower();
                    string number = Regex.Match(choice, @"\d+").Value;
                    string pageNum = "p" + number;
                    if (choice == "n")
                    {
                        if (page == pages)
                        {
                            WaitForButton("Không có trang sau! Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = page + 1;
                            k = k + 10;
                        }
                    }
                    else if (choice == "p")
                    {
                        if (page == 1)
                        {
                            WaitForButton("Không có trang trước! Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = page - 1;
                            k = k - 10;
                        }
                    }
                    else if (choice == pageNum)
                    {
                        if (int.Parse(number) < 0 || int.Parse(number) > pages || int.Parse(number) == 0)
                        {
                            Console.WriteLine($"Không tồn tại trang {int.Parse(number)}");
                            WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = int.Parse(number);
                            k = (int.Parse(number) - 1) * 10;
                        }
                    }
                    else if (choice == "0") return;
                    else
                    {
                        bool found = false;
                        string search1 = '"' + choice + '"';
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            try
                            {
                                if (int.Parse(choice) == list[i].bookId)
                                {
                                    ShowBookDetail(list[i], search1);
                                    WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                                    found = true;
                                    break;
                                }
                            }
                            catch (System.FormatException) { }
                            catch (System.ArgumentOutOfRangeException) { }
                        }
                        if (!(found))
                        {
                            Console.WriteLine("ID không phù hợp!");
                            WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        }
                    }
                }
            }
        }

        private void ShowBookDetail(Book book, string search)
        {
            Console.Clear();
            string price = FormatCurrency(book.bookPrice.ToString());
            Console.WriteLine("===============================================================================================");
            Console.WriteLine($"|                             Thông tin chi tiết sách có mã là {search,-30} |");
            Console.WriteLine("===============================================================================================");
            Console.WriteLine($"| Mã sách:           | {book.bookId,-70} |");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Tên sách:          | {book.bookName,-70} |");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Giá:               | {price,-70} |");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Phân loại:         | {book.bookCategory,-70} |");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Số lượng:          | {book.bookQuantity,-70} |");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.Write("| Mô tả:             |");
            string str = ' ' + book.bookDescription;
            string subStr;
            int i = 65;
            try
            {
                while (str.Length > 0 && i < str.Length)
                {
                    i = 65;
                    while (str[i] != ' ')
                    {
                        i = i + 1;
                        if (i >= str.Length) break;
                    }
                    subStr = str.Substring(1, i);
                    Console.WriteLine($" {subStr,-70} |");
                    Console.Write("|                    |");
                    str = str.Remove(0, i);
                }
            }
            catch (System.ArgumentOutOfRangeException) { }
            finally
            {
                Console.WriteLine($" {str.Remove(0, 1),-70} |");
                Console.WriteLine("===============================================================================================");
            }
        }

        public string FormatCurrency(string currency)
        {
            for (int k = currency.Length - 3; k > 0; k = k - 3)
            {
                currency = currency.Insert(k, ".");
            }
            currency = currency + " VND";
            return currency;
        }

        void WaitForButton(string msg)
        {
            Console.Write(msg);
            Console.ReadKey();
        }
    }
}