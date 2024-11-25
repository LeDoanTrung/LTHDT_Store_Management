using DoAnCuoiKy_KTLT.Entiy;

namespace DoAnCuoiKy_KTLT.XyLyLuuTru
{
    public class LuuTruUser
    {
        public static User [] ReadUserList()
        {
            User[] userList;


            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string filePath = Path.Combine(projectRoot, "resource", "user.txt");
            StreamReader file = new StreamReader(filePath);

            string s = file.ReadLine();

            int n = int.Parse(s);

            userList = new User[n];

            for (int i = 0; i < n; i++)
            {
                s = file.ReadLine();
                string[] m = s.Split(";");
                userList[i].Id = int.Parse(m[0]);
                userList[i].Name = m[1];
                userList[i].Password = m[2];

            }
            file.Close();
            return userList;
        }


        public static bool AddNewUser(User us)
        {
            User[] danhSachUser = ReadUserList();
            User[] dsMoi = new User[danhSachUser.Length + 1];

            for (int i = 0; i < danhSachUser.Length; i++)
            {
                dsMoi[i] = danhSachUser[i];
            }

            dsMoi[dsMoi.Length - 1] = us;
            SaveListToDB(dsMoi);

            return true;
        }

        public static void SaveListToDB(User[] userList)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string filePath = Path.Combine(projectRoot, "resource", "user.txt");
            
            StreamWriter file = new StreamWriter(filePath);

            file.WriteLine(userList.Length);
            for (int i = 0; i < userList.Length; i++)
            {
                file.WriteLine($"{userList[i].Id};{userList[i].Name};{userList[i].Password}");
            }
            file.Close();
        }

        public static User? EditUser(User cate)
        {
            User[] list = ReadUserList();

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Id == cate.Id)
                {
                    list[i] = cate;

                    SaveListToDB(list);
                    return cate;
                }
            }

            return null;
        }
    }
}
