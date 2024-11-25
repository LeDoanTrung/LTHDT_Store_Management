using DoAnCuoiKy_KTLT.Entiy;
using DoAnCuoiKy_KTLT.XyLyLuuTru;
namespace DoAnCuoiKy_KTLT.XuLyNghiepVu
{
    public class XuLyUser
    {
        
        public static User? CreateUser(int id, string username, string password)
        {
            bool laHopLe = true;

            if (id < 0)
            {
                laHopLe = false;
            }
            if (username.Length < 3)
            {
                laHopLe = false;
            }
            if (password.Length < 8)
            {
                laHopLe = false;
            }

            if (laHopLe)
            {
                User nd;
                nd.Id = id;
                nd.Name = username;
                nd.Password = password;
                return nd;
            }

            return null;
        }

        public static bool AddNewUser(User nd)
        {
            return LuuTruUser.AddNewUser(nd);
        }


        public static User? Login(string username, string password)
        {
            bool laHopLe = true;
            if (username.Length < 3)
            {
                laHopLe = false;
            }
            if (password.Length < 8)
            {
                laHopLe = false;
            }

            if (laHopLe)
            {
                User[] dsnd = LuuTruUser.ReadUserList();
                foreach (User user in dsnd)
                {
                    if (user.Password == password && user.Name == username)
                    {
                        return user;
                    }
                }

            }

            return null;
        }

        public static User[] ReadUserList(string keyword="")
        {
            User[] proList = LuuTruUser.ReadUserList();

            User[] newProList;

            int n = 0;

            for (int i = 0; i < proList.Length; i++)
            {
                if (proList[i].Name.Contains(keyword) || proList[i].Id == int.Parse(keyword))
                {
                    n += 1;
                }
            }

            newProList = new User[n];

            int j = 0;

            for (int i = 0; i < proList.Length; i++)
            {
                if (proList[i].Name.Contains(keyword) || proList[i].Id == int.Parse(keyword))
                {
                    newProList[j] = proList[i];
                    j++;
                }
            }

            return newProList;
        }
        // Phương thức sinh mã user tự động
        public static int GenerateUserId()
        {
            User[] dssp = XuLyUser.ReadUserList();
            // Lấy danh sách mã user hiện có
            var existingIds = dssp.Select(c => c.Id).ToList();

            // Tìm mã user lớn nhất
            int maxId = existingIds.Any() ? existingIds.Max() : 0;

            // Tạo mã suser mới bằng cách tăng mã user lớn nhất lên 1
            return maxId + 1;
        }


        public static bool IsUsernameExists(string name)
        {
            User[] userList = XuLyUser.ReadUserList();

            foreach (User user in userList)
            {
                if (user.Name.Equals(name))
                {
                    return true;
                }
            }

            return false;
        }


        public static User? SearchUserByName(string name)
        {
            User[] list = LuuTruUser.ReadUserList();

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Name.Equals(name))
                {
                    return list[i];
                }
            }

            return null;
        }

        public static User? EditUser(string name, string newName, string password)
        {
            User? user = SearchUserByName(name);

            if (user.HasValue)
            {
                User newUser = user.Value;
                newUser.Name = newName;
                newUser.Password = password;

                return LuuTruUser.EditUser(newUser);
            }

            return null;
        }

        public static bool CheckPassword(string name, string pass)
        {
            User? user = SearchUserByName(name);

            if (user.HasValue)
            {
                if (user.Value.Password.Equals(pass))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
