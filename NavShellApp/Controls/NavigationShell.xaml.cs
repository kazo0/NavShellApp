using Microsoft.UI.Xaml.Markup;
using Uno.Disposables;

namespace NavShellApp;

public sealed partial class NavigationShell : UserControl
{
	public static class TemplateParts
	{
		public const string TabBarName = "TabBar";
		public const string NavigationViewName = "NavView";
		public const string NavigationGridName = "NavigationGrid";
	}

	#region NavigationItems DependencyProperty
	public IList<ShellItem> NavigationItems
	{
		get { return (IList<ShellItem>)GetValue(NavigationItemsProperty); }
		set { SetValue(NavigationItemsProperty, value); }
	}

	public static readonly DependencyProperty NavigationItemsProperty =
		DependencyProperty.Register("NavigationItems", typeof(IList<ShellItem>), typeof(NavigationShell), new PropertyMetadata(new List<ShellItem>(), OnNavigationItemsChanged));

	private static void OnNavigationItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is NavigationShell navigationShell)
		{
			navigationShell.OnNavigationItemsChanged();
		}
	}
	#endregion

	private SerialDisposable _eventSubscriptions = new();

	public NavigationShell()
	{
		this.InitializeComponent();

		Loaded += OnLoaded;
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		DisposeSubscriptions();

		var disposables = new CompositeDisposable();
		_eventSubscriptions.Disposable = disposables;

		NavView.SelectionChanged += OnNavViewSelectionChanged;
		TabBar.SelectionChanged += OnTabBarSelectionChanged;
		disposables.Add(Disposable.Create(() => TabBar.SelectionChanged -= OnTabBarSelectionChanged));

		OnNavigationItemsChanged();
	}

	private void OnNavViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
	{
		if (args.SelectedItem is NavigationViewItem navItem)
		{
			MainFrame.Navigate(typeof(FirstPage));
		}
	}

	private void DisposeSubscriptions()
	{
		_eventSubscriptions?.Dispose();
	}

	private void OnTabBarSelectionChanged(TabBar sender, TabBarSelectionChangedEventArgs args)
	{
		if (args.NewItem is TabBarItem tabBarItem)
		{
			if (tabBarItem.Tag is ShellItem shellItem)
			{
				var page = shellItem.Content as Page;
				if (page is not null)
				{
					MainFrame.Navigate(page.GetType());
				}
			}
		}
	}

	private void OnNavigationItemsChanged()
	{

		TabBar.Items.Clear();
		foreach (var item in NavigationItems)
		{
			TabBar.Items.Add(new TabBarItem
			{
				Content = item.Title,
				Icon = item.Icon,
				Tag = item
			});
		}

		//NavItemsRepeater.ItemsSource = NavigationItems;
	}
}
