using System.Runtime.CompilerServices;

namespace BiTrend.ViewModels;

internal class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public MainViewModel()
    {
        Categories = new ObservableCollection<Category>();
        //IsLoadingCategories = true;

        //Task.Run(async () =>
        //{
        //    await InitializeCategories();
        //}).GetAwaiter().OnCompleted(() =>
        //{
        //    IsLoadingCategories = false;
        //});
    }

    private ObservableCollection<Category> _categories;
    private bool _isLoadingCategories;

    public ObservableCollection<Category> Categories
    {
        get => _categories;
        set
        {
            _categories = value;
            OnPropertyChanged();
        }
    }

    public bool IsLoadingCategories
    {
        get => _isLoadingCategories;
        set
        {
            _isLoadingCategories = value;
            OnPropertyChanged();
        }
    }

    private async Task InitializeCategories()
    {
        Categories.Clear();

        try
        {
            var response = await AppServiceProvider.Instance().GetCategories();

            if (response != null)
            {
                foreach (var item in response)
                    Categories.Add(item);
            }
        }
        catch (Exception ex) 
        {

        }
        //finally
        //{
        //    IsLoadingCategories = false;
        //}
    }

    public void OnAppearing()
    {
        IsLoadingCategories = true;

        Task.Run(async () =>
        {
            await InitializeCategories();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsLoadingCategories = false;
        });
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
