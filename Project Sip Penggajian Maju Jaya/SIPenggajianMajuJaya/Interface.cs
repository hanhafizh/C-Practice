using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

// Farhan Hafizh - 50420460

namespace SIPenggajianMajuJaya
{
    class Interface
    {
        private const string GARIS = "\t\t--------------------------------------";
        private const string HOLDER = "<<press\"enter\" to continue to main menu >>";
        private ArrayList kehadiran;
        private Pegawai pegawai;
        private PegawaiTetap pegawaiTetap;
        private PegawaiHonorer pegawaiHonorer;
        string[] nama = { "farhan", "ruzannah", "hamas", "afra", "upa" };
        string[] status = { "honorer", "tetap", "tetap", "honorer", "honorer" };

        public Interface()
        {
            pegawai = new Pegawai();
            pegawaiTetap = new PegawaiTetap();
            pegawaiHonorer = new PegawaiHonorer();
            kehadiran = new ArrayList();
            pegawaiTetap.getGaji();
            pegawai.InitializeData(this.nama, this.status);
            pegawaiHonorer.InitializeData(this.nama, this.status);
            pegawaiTetap.InitializeData(this.nama, this.status);
        }
        public void MainMenu()
        {
            //Header
            Console.WriteLine(GARIS);
            Console.WriteLine("\t\t Sistem Pengolahan Gaji Pegawai PT Gunadarma Jaya");
            Console.WriteLine(GARIS);
            Console.WriteLine("Menu: ");
            Console.WriteLine(" 1) Input Kehadiran Pegawai");
            Console.WriteLine(" 2) Lihat Gaji Pegawai");
            Console.WriteLine(" 3) Tentang Aplikasi");
            Console.WriteLine(" 4) Exit");
            //User Input
            try
            {
                Console.WriteLine();
                Console.WriteLine("Pilih>");
                int pilihan = Convert.ToInt16(Console.ReadLine());
                switch (pilihan)
                {
                    case 1:
                        Console.Clear();
                        AttendanceMenu();
                        break;
                    case 2:
                        Console.Clear();
                        SalaryMenu();
                        break;
                    case 3:
                        Console.Clear();
                        About();
                        break;
                    case 4:
                        Exit();
                        break;
                    default:
                        Console.WriteLine(GARIS);
                        Console.WriteLine("Error: masukkan pilihan 1-4");
                        MainMenu();
                        break;
                }
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine(GARIS);
                Console.WriteLine("Error: invalid input"); MainMenu();
            }
        }
        public void SalaryMenu()
        {
            Console.Clear();
            if (this.kehadiran.Count > 0)
            {
                ArrayList status = this.pegawai.getStatus();
                ArrayList gajiHonorer = this.pegawaiHonorer.getGaji();
                ArrayList gajiTetap = this.pegawaiTetap.getGaji();
                int n = 1;
                Console.WriteLine("{0}", GARIS);
                Console.WriteLine("\t\t\t\t DAFTAR PEGAWAI", GARIS);
                Console.WriteLine("{0}\n", GARIS);
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("|No\t| Nama\t\t| Status\t| Gaji\t\t|");
                Console.WriteLine("--------------------------------------");
                foreach (string element in pegawai.getPegawai())
                {
                    if (Convert.ToString(status[n - 1]) == "tetap")
                    {
                        Console.WriteLine("| {0} \t| {1} \t| {2} \t| {3} \t|", n, element, status[n - 1], gajiTetap[n - 1]);
                    }
                    else
                    {
                        Console.WriteLine("| {0} \t| {1} \t| {2} \t| {3} \t|", n, element, status[n - 1], gajiHonorer[n - 1]);
                    }
                    n++;
                }
                Console.WriteLine("--------------------------------------");
            }
            else
            {
                Console.WriteLine("Belum ada data absensi yang masuk");
                Console.WriteLine();
            }
            Console.WriteLine("\n{0}", GARIS);
            Console.WriteLine("\n\n\n\n\n\n{0}", HOLDER);
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }
        public void AttendanceMenu()
        {
            Console.WriteLine("{0}", GARIS);
            Console.WriteLine("\t\t\t\t KEHADIRAN PEGAWAI", GARIS);
            Console.WriteLine("{0}\n", GARIS);
            int n = 0;
            foreach (String element in pegawai.getPegawai())
            {
                try
                {
                    Console.Write("{0}. Masukkan jumlah hari kerja {1}: ", n + 1, element);
                    int hari = Convert.ToInt16(Console.ReadLine());
                    this.kehadiran.Add(hari);
                }
                catch (Exception)
                {
                    Console.Clear();
                    this.kehadiran.Clear();
                    Console.WriteLine("Error: Masukkan hari dengan angka 1-30");
                    AttendanceMenu();
                }
                n++;
            }
            pegawaiHonorer.setKehadiran(this.kehadiran);
            pegawaiTetap.setKehadiran(this.kehadiran);
            Console.WriteLine("\n\n\n\n\n\n{0}", HOLDER);
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }
        public void LoginMenu()
        {
            //Header
            Console.WriteLine(GARIS);
            Console.WriteLine("\t\t\t\t LOGIN", GARIS);
            Console.WriteLine(GARIS);
            //User Input
            Console.Write("\t\t Username:");
            string username = Console.ReadLine();
            Console.Write("\t\t Password:");
            string password = Console.ReadLine();
            Console.WriteLine("{0}", GARIS);
            if (this.pegawai.Login(username, password))
            {
                Console.Write(this.pegawai.getNotification());
                for (int i = 0; i < 3; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(1000);
                }
                Console.WriteLine();
                MainMenu();
            }
            else
            {
                username = null;
                password = null;
                Console.Clear();
                Console.WriteLine(this.pegawai.getNotification());
                LoginMenu();
            }
        }
        public void About()
        {
            Console.Clear();
            Console.WriteLine("{0}", GARIS);
            Console.WriteLine("\t\t\t\t TENTANG SISTEM", GARIS);
            Console.WriteLine("{0}", GARIS);
            Console.WriteLine("\t Sistem ini merupakan sistem penggajian karyawan milik PT Gunadarma Jaya", GARIS);
            Console.WriteLine("\t\t\t Dibuat oleh Univ Gunadarma");
            Console.WriteLine("\t\t\t\t Tahun 2022");
            Console.WriteLine("\n{0}", GARIS);
            Console.WriteLine("\n\n\n\n\n\n{0}", HOLDER);
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }
        public void Exit()
        {
            Console.WriteLine("\n\n\n BYE");
        }
    }
}