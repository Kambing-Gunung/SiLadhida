using Avalonia.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Threading.Tasks;
using SiLadhida.App.Services; 

namespace SiLadhida.App.Views
{
    public partial class CreateOrderDialog : Window, INotifyPropertyChanged
    {
        private string _newOrderName = string.Empty;
        
        // Memanggil service produk untuk mengambil data dari database
        private readonly ProductApiService _productService = new();

        public string NewOrderName
        {
            get => _newOrderName;
            set { _newOrderName = value; OnPropertyChanged(); }
        }

        // List yang akan mengikat ke UI Checkbox
        public ObservableCollection<SelectableProductItem> ProductList { get; set; } = new();

        public CreateOrderDialog()
        {
            InitializeComponent();
            DataContext = this;
            
            // Panggil API saat dialog pertama kali dibuka
            _ = LoadProductsFromDatabaseAsync();
        }

        private async Task LoadProductsFromDatabaseAsync()
        {
            try
            {
                // Ambil daftar produk dari API Backend
                var products = await _productService.GetProductsAsync();
                
                if (products != null)
                {
                    foreach (var p in products)
                    {
                        // Masukkan ke dalam list pembungkus agar punya status 'IsSelected' (Dicentang)
                        ProductList.Add(new SelectableProductItem 
                        { 
                            ProductId = p.Id, 
                            // Catatan: Jika di modelmu namanya 'Name', ubah p.Nama menjadi p.Name
                            ProductName = p.Nama, 
                            Harga = p.Harga 
                        });
                    }
                }
            }
            catch 
            {
                // Tangani error jika koneksi ke database produk gagal
            }
        }

        public void CloseDialogCommand()
        {
            this.Close(false);
        }

        public void SubmitOrderCommand()
        {
            // 1. Validasi Nama Pemesan tidak boleh kosong atau spasi doang
            if (string.IsNullOrWhiteSpace(NewOrderName))
            {
                SiLadhida.App.App.Notification.ShowError("Nama pemesan wajib diisi!");
                return;
            }

            // 2. Validasi harus ada minimal 1 produk yang dicentang dengan kuantitas > 0
            var checkedItems = ProductList.Where(p => p.IsSelected && p.Quantity > 0).ToList();
            if (checkedItems.Count == 0)
            {
                SiLadhida.App.App.Notification.ShowError("Pilih minimal 1 produk untuk dipesan!");
                return; 
            }

            // Jika semua valid, tutup dialog dan kirim sinyal sukses (true)
            this.Close(true); 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Kelas bantuan khusus untuk mengontrol Checkbox dan Kuantitas di UI
    public class SelectableProductItem : INotifyPropertyChanged
    {
        private bool _isSelected;
        private int _quantity = 1;

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Harga { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}