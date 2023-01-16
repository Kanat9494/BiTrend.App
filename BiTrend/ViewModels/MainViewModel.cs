using BiTrend.Models;
using System.Runtime.CompilerServices;

namespace BiTrend.ViewModels;

internal class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public MainViewModel()
    {
        IsLoading = true;
        Categories = new ObservableCollection<Category>();
        PopularProducts = new ObservableCollection<PopularProduct>();

        LoadContent();
    }

    private void LoadContent()
    {
        Task.Run(async () =>
        {
            await InitializeCategories();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsLoading = false;
        });

        Task.Run(async () =>
        {
            await InitializeTopProducts();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsLoading = false;
        });
    }

    private ObservableCollection<Category> _categories;
    private bool _isLoading;
    private ObservableCollection<PopularProduct> _popularProducts;

    public ObservableCollection<Category> Categories
    {
        get => _categories;
        set
        {
            _categories = value;
            OnPropertyChanged();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<PopularProduct> PopularProducts
    {
        get => _popularProducts;
        set
        {
            _popularProducts = value;
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

    private async Task InitializeTopProducts()
    {
        try
        {
            var response = await AppServiceProvider.Instance().GetPopularProducts();

            if (response != null)
            {
                foreach (var item in response)
                    PopularProducts.Add(item);
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void OnAppearing()
    {
        Task.Run(async () =>
        {
            await InitializeCategories();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsLoading = false;
        });

        Task.Run(async () =>
        {
            await InitializeTopProducts();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsLoading = false;
        });
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
