using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace ReduxSimple.Samples.Extensions
{
    public static class IObservableExtensions
    {
        public static IObservable<T> WithCache<T>(this IObservable<T> source, Func<T> get, Action<T> put) where T : class
        {
            return Observable.Create<T>(observer =>
            {
                var cached = get();
                if (cached != null)
                {
                    observer.OnNext(cached);
                }
                else
                {
                    source.Subscribe(item =>
                    {
                        put(item);
                        observer.OnNext(item);
                    }, observer.OnError, observer.OnCompleted);
                }

                return Disposable.Empty;
            });
        }

        public static IObservable<EventPattern<RoutedEventArgs>> ObserveOnClick(this ButtonBase button)
        {
            return Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => button.Click += h,
                h => button.Click -= h
            );
        }

        public static IObservable<EventPattern<TappedRoutedEventArgs>> ObserveOnTapped(this UIElement element)
        {
            return Observable.FromEventPattern<TappedEventHandler, TappedRoutedEventArgs>(
                h => element.Tapped += h,
                h => element.Tapped -= h
            );
        }

        public static IObservable<EventPattern<RoutedEventArgs>> ObserveOnLostFocus(this UIElement element)
        {
            return Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => element.LostFocus += h,
                h => element.LostFocus -= h
            );
        }

        public static IObservable<EventPattern<AutoSuggestBoxTextChangedEventArgs>> ObserveOnTextChanged(this AutoSuggestBox autoSuggestBox)
        {
            return Observable.FromEventPattern<TypedEventHandler<AutoSuggestBox, AutoSuggestBoxTextChangedEventArgs>, AutoSuggestBoxTextChangedEventArgs>(
                h => autoSuggestBox.TextChanged += h,
                h => autoSuggestBox.TextChanged -= h
            );
        }

        public static IObservable<EventPattern<AutoSuggestBoxSuggestionChosenEventArgs>> ObserveOnSuggestionChosen(this AutoSuggestBox autoSuggestBox)
        {
            return Observable.FromEventPattern<TypedEventHandler<AutoSuggestBox, AutoSuggestBoxSuggestionChosenEventArgs>, AutoSuggestBoxSuggestionChosenEventArgs>(
                h => autoSuggestBox.SuggestionChosen += h,
                h => autoSuggestBox.SuggestionChosen -= h
            );
        }
    }
}
