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
            if (book.bookId <= 0)
            {
                Console.WriteLine($"Không tồn tại sản phẩm phù hợp với từ khoá là '{searchKeyWord}'");
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
            list = bookDal.GetBook(list, commandText);
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
                string chosse, price;
                for (; ; )
                {
                    Console.Clear();
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine("|                                                       Tất cả sách                                                             |");
                    Console.WriteLine($"|                                                                                                                    Trang {page}/{pages} |");
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine("| Mã Sách | Tên sách                                                            | Giá           | Loại          |");
                    if (list.Count < size)
                    {
                        for (i = 0; i < list.Count; i++)
                        {
                            price = FormatCurrency(list[i].bookPrice.ToString());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"| {list[i].bookId,5} | {list[i].bookName,-65} | {price,9} | {list[i].bookCategory,-13} |");
                        }
                    }
                    else
                    {
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            if (i == list.Count) break;
                            price = FormatCurrency(list[i].bookPrice.ToString());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"| {list[i].bookId,5} | {list[i].bookName,-65} | {price,13} | {list[i].bookCategory,-13} |");
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
                    chosse = Console.ReadLine() ?? "";
                    while (!(Regex.IsMatch(chosse, @"([PpNn]|[1-9]|^0$)")))
                    {
                        Console.Write("Lựa chọn không hợp lệ! Chọn lại: ");
                        chosse = Console.ReadLine() ?? "";
                    }
                    chosse = chosse.Trim();
                    chosse = chosse.ToLower();
                    string number = Regex.Match(chosse, @"\d+").Value;
                    string pageNum = "p" + number;
                    if (chosse == "n")
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
                    else if (chosse == "p")
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
                    else if (chosse == pageNum)
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
                    else if (chosse == "0")
                    {
                        return;
                    }
                    else
                    {
                        bool found = false;
                        string search1 = '"' + chosse + '"';
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            try
                            {
                                if (int.Parse(chosse) == list[i].bookId)
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
            list = bookDal.GetBook(list, commandText);
            string search = searchKeyWord;
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
                    Console.WriteLine("|                                            Ket qua tim kiem voi tu khoa la {0,-49} |", search);
                    Console.WriteLine($"| Tim thay khoang {list.Count,3} vat pham                                                                                       Trang {page}/{pages} |");
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine("| Ma SP | Ten san pham                                                      | Gia           | Loai          |");
                    if (list.Count < size)
                    {
                        for (i = 0; i < list.Count; i++)
                        {
                            price = FormatCurrency(list[i].bookPrice.ToString());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"| {list[i].bookId,5} | {list[i].bookName,-65} | {price,13} | {list[i].bookCategory,-13} |");
                        }
                    }
                    else
                    {
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            if (i == list.Count) break;
                            price = FormatCurrency(list[i].bookPrice.ToString());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"| {list[i].bookId,5} | {list[i].bookName,-65} | {price,13} | {list[i].bookCategory,-13} |");
                        }
                    }
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine(" Nhan P để xem trang truoc.");
                    Console.WriteLine(" Nhan N để xem trang tiep theo.");
                    Console.WriteLine(" Nhap P kem so trang để xem trang mong muon (VD: P1, P2,...).");
                    Console.WriteLine(" Nhap ID để xem chi tiet san pham.");
                    Console.WriteLine(" Nhan 0 để quay lai.");
                    Console.WriteLine("-----------------------------------------------------------------");
                    Console.Write(" Chon: ");
                    choice = Console.ReadLine() ?? "";
                    while (!(Regex.IsMatch(choice, @"([PpNn]|[1-9]|^0$)")))
                    {
                        Console.Write(" Chon khong hop le! Chon lai: ");
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
                            Console.Write(" Khong co trang sau! Nhan bat ki phim nao de tiep tuc...");
                            Console.ReadKey();
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
                            Console.Write(" Khong co trang truoc! Nhan bat ki phim nao de tiep tuc...");
                            Console.ReadKey();
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
                            Console.WriteLine(" Khong ton tai trang {0}", int.Parse(number));
                            Console.Write(" Nhan bat ki phim nao de tiep tuc...");
                            Console.ReadKey();
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
                                    Console.Write(" Nhan phim bat ki de tiep tuc...");
                                    Console.ReadKey();
                                    found = true;
                                    break;
                                }
                            }
                            catch (System.FormatException) { }
                            catch (System.ArgumentOutOfRangeException) { }
                        }
                        if (!(found))
                        {
                            Console.WriteLine(" ID khong phu hop!");
                            Console.Write(" Nhan phim bat ky de tiep tuc...");
                            Console.ReadKey();
                        }
                    }
                }
            }
        }

        internal void ShowBookDetail(Book book, string search)
        {
            Console.Clear();
            string price = FormatCurrency(book.bookPrice.ToString());
            Console.WriteLine("===============================================================================================");
            Console.WriteLine($"|                             Thong tin chi tiet san pham co ma la {search,-26} |");
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
                    Console.WriteLine(" {0,-70} |", subStr);
                    Console.Write("|                    |");
                    str = str.Remove(0, i);
                }
            }
            catch (System.ArgumentOutOfRangeException) { }
            finally
            {
                Console.WriteLine(" {0,-70} |", str.Remove(0, 1));
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