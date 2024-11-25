using DoAnCuoiKy_KTLT.Entiy;
using DoAnCuoiKy_KTLT.XuLyNghiepVu;


namespace DoAnCuoiKy_KTLT.XyLyLuuTru
{
    public static class LuuTruProduct
    {
        //Đọc danh sách Sản Phẩm
        public static Product[] ReadProduct()
        {

            Product[] productList;

            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string filePath = Path.Combine(projectRoot, "resource", "product.txt");
            StreamReader file = new StreamReader(filePath);

            string s = file.ReadLine();
            int n = int.Parse(s);
            productList = new Product[n];

            for (int i = 0; i < n; i++)
            {
                s = file.ReadLine();
                string[] m = s.Split(";");
                productList[i].Id = int.Parse(m[0]);
                productList[i].Name = m[1];
                productList[i].DueDate = DateOnly.Parse(m[2]);
                productList[i].CompName = m[3];
                productList[i].DateManufacrure = DateOnly.Parse(m[4]);
                productList[i].CateId = int.Parse(m[5]);
                productList[i].Price = int.Parse(m[6]);
                productList[i].Quantity = int.Parse(m[7]);
            }
            file.Close();
            return productList;

        }

        //Tìm kiếm Product theo Id
        public static Product? SearchProductById(int id)
        {
            Product[] productList = ReadProduct();
            
            foreach (Product product in productList)
            {
                if (product.Id == id)
                {
                    return product;
                }
            }

            return null;
        }

        //Tìm kiếm product theo Name
        public static Product? SearchProductByName(string name)
        {
            Product[] productList = ReadProduct();

            foreach (Product product in productList)
            {
                if (product.Name.Contains(name))
                {
                    return product;
                }
            }

            return null;
        }

        //Thêm mới một Product
        public static bool AddNewProduct(Product p)
        {
            Product[] productList = ReadProduct();

            Product[] newProList = new Product[productList.Length+1];
            
            for (int i = 0;i < productList.Length; i++)
            {
                newProList[i] = productList[i];
            }

            newProList[newProList.Length - 1] = p;
            SaveToDatabase(newProList);

            return true;
        }

        //Luu danh sách mới cập nhật xuống DB
        public static void SaveToDatabase(Product[] list)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string filePath = Path.Combine(projectRoot, "resource", "product.txt");
             
            StreamWriter file = new StreamWriter(filePath);

            file.WriteLine(list.Length);

            for (int i = 0; i < list.Length; i++)
            {
                file.WriteLine($"{list[i].Id};{list[i].Name};{list[i].DueDate};{list[i].CompName};{list[i].DateManufacrure};{list[i].CateId};{list[i].Price};{list[i].Quantity}");
            }
            file.Close();
        }

        // Sửa sản phẩm
        public static Product? EditProduct (Product p)
        {
            Product[] proList = ReadProduct();
            
            for (int i = 0; i < proList.Length  ; i++)
            {
                if (proList[i].Id == p.Id)
                {
                    proList[i] = p;
                    SaveToDatabase(proList);
                    return p;
                }
            }

            return null;

        }

        //Xóa sản phẩm
        public static void DeleteProduct (int id) {
            Product[] proList = ReadProduct();
            Product[] newProList = new Product[proList.Length-1];

            int j = 0;

            for (int i = 0; i < proList.Length; i++)
            {
                if (proList[i].Id != id)
                {
                    newProList[j] = proList[i];
                    j++;
                }
            }

            SaveToDatabase (newProList);
        }


        public static void UpdateStockQuantityInDatabase(int id, int quantity)
        {
            Product[] list = XuLyProduct.ReadProductList();

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Id == id)
                {
                    list[i].Quantity -= quantity;
                }
            }

            SaveToDatabase(list);
        }


        //Hoàn lại quantuty khi Edit Sale bill
        public static void RestockQuantityWhileEditSaleBill(int id, int quantity)
        {
            Product[] list = XuLyProduct.ReadProductList();

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Id == id)
                {
                    list[i].Quantity += quantity;
                }
            }

            SaveToDatabase(list);
        }
    }
}
