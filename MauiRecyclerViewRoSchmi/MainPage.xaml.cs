namespace MauiRecyclerViewRoSchmi
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        MainViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();

            
            BindingContext = _viewModel = new MainViewModel();

            _viewModel.InitializeProducts();

            int breakpoint18 = 1;

            //Task.Run(async () =>
            //{
            //    await _viewModel.InitializeProducts();
            //});
        }       
    }
}
