using DoAnCuoiKy_KTLT.Pages.Product;
using DoAnCuoiKy_KTLT.XyLyLuuTru;
using DoAnCuoiKy_KTLT.Entiy;
using Microsoft.VisualBasic;

namespace DoAnCuoiKy_KTLT.XuLyNghiepVu
{
    public class XuLyProduct
    {
        public static Product[] ReadProductList(string keyword = "")
        {
            Product[] proList = LuuTruProduct.ReadProduct();

            Product[] newProList;

            int n = 0;

            for (int i = 0; i < proList.Length; i++)
            {
                if (proList[i].Name.Contains(keyword) || proList[i].Id.ToString() == keyword)
                {
                    n += 1;
                }
            }

            newProList = new Product[n];

            int j = 0;

            for (int i = 0; i < proList.Length; i++)
            {
                if (proList[i].Name.Contains(keyword) || proList[i].Id.ToString() == keyword)
                {
                    newProList[j] = proList[i];
                    j++;
                }
            }

            return newProList;
        }

        public static Product? CreateProduct(int id, string name, DateOnly dueDate, string compName, DateOnly dateManu, int cateId, int price)
        {
            if (id > 0 && !string.IsNullOrEmpty(name) && dueDate > DateOnly.MinValue && dueDate < DateOnly.MaxValue
                && !string.IsNullOrEmpty(compName) && dateManu > DateOnly.MinValue && dateManu < DateOnly.MaxValue &&
                price > 0 && cateId > 0)
            {
                Product p;

                p.Id = id;
                p.Name = name;
                p.DueDate = dueDate;
                p.CompName = compName;
                p.DateManufacrure = dateManu;
                p.CateId = cateId;
                p.Price = price;
                p.Quantity = 0;

                return p;
            }


            return null;

        }

        public static bool AddProduct(Product p)
        {
            return LuuTruProduct.AddNewProduct(p);
        }


        public static Product? SearchProductById(int id)
        {
            Product? p = LuuTruProduct.SearchProductById(id);
            return p;
        }

        public static Product? SearchProductByName(string name)
        {
            Product? p = LuuTruProduct.SearchProductByName(name);
            return p;
        }

        public static Product? EditProduct(int id, string name, DateOnly dueDate, string compName, DateOnly dateManu, int cateId, int price)
        {
            Product? p = LuuTruProduct.SearchProductById(id);
            if (p.HasValue)
            {
                Product newProduct = p.Value;
                newProduct.Name = name;
                newProduct.DueDate = dueDate;
                newProduct.CompName = compName;
                newProduct.DateManufacrure = dateManu;
                newProduct.Price = price;
                newProduct.CateId = cateId;
                

                return LuuTruProduct.EditProduct(newProduct);
            }

            return null;
        }

        public static void DeleteProduct(int id)
        {

            LuuTruProduct.DeleteProduct(id);


        }


        // Phương thức sinh mã sản phẩm tự động
        public static int GenerateProductId()
        {
            Product[] dssp = XuLyProduct.ReadProductList();
            // Lấy danh sách mã sản phẩm hiện có
            var existingIds = dssp.Select(c => c.Id).ToList();

            // Tìm mã sản phẩm lớn nhất
            int maxId = existingIds.Any() ? existingIds.Max() : 0;

            // Tạo mã sản phẩm mới bằng cách tăng mã sản phẩm lớn nhất lên 1
            return maxId + 1;
        }


        public static Product[] GetProductsByCategoryId(int categoryId)
        {

            Product[] proList = ReadProductList();

            Product[] newList;

            int n = 0;

            foreach (Product product in proList)
            {
                if (product.CateId == categoryId)
                {
                    n += 1;
                }
            }

            newList = new Product[n];

            int j = 0;

            for (int i = 0; i < proList.Length; i++)
            {
                if (proList[i].CateId == categoryId)
                {
                    newList[j] = proList[i];
                    j++;
                }
            }

            return newList;
        }

        public static Product? SearchProduct(string tensp, DateOnly dueDate, string compName, DateOnly dateManufacture, int cateId, int gia)
        {
            Product[] allProducts = ReadProductList();


            foreach (Product product in allProducts)
            {

                if (product.Name.Equals(tensp) &&
                    product.DueDate.Equals(dueDate) &&
                    product.CompName.Equals(compName) &&
                    product.DateManufacrure.Equals(dateManufacture) &&
                    product.CateId == cateId &&
                    product.Price == gia)
                {

                    return product;
                }
            }


            return null;
        }


        // Kiểm tra số lượng tồn kho trước khi thêm sản phẩm vào hóa đơn
        public static int GetQuantityInStock(int id)
        {
            Product[] list = XuLyProduct.ReadProductList();

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Id == id)
                {
                    return list[i].Quantity;
                }
            }


            return -1;
        }

        public static void UpdateStockQuantityInDatabase(int id, int quantity)
        {
            LuuTruProduct.UpdateStockQuantityInDatabase(id, quantity);
        }

        public static void RestockQuantityWhileEditSaleBill(int id, int quantity)
        {
            LuuTruProduct.RestockQuantityWhileEditSaleBill(id, quantity);
        }


        //Kiểm tra duplicate
        public static bool CheckDuplicateProduct(Product product)
        {
            Product[] productList = ReadProductList();

            foreach (var p in productList)
            {
                if (p.Name == product.Name &&
                    p.CompName == product.CompName &&
                    p.DateManufacrure == product.DateManufacrure &&
                    p.DueDate == product.DueDate &&
                    p.Price == product.Price)
                {
                    return true; // Tồn tại sản phẩm trùng
                }
            }

            return false; // Không tồn tại sản phẩm trùng
        }
    }

}