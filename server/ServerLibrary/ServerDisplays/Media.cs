using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ServerLibrary.ServerDisplays
{
    public static class Media
    {
        public static readonly DependencyProperty MediaSourceProperty =
            DependencyProperty.RegisterAttached(
                "MediaSource",
                typeof(Uri),
                typeof(Media),
                new PropertyMetadata(OnMediaSourceChanged)
                );

        public static readonly DependencyProperty PlayerStateProperty =
            DependencyProperty.RegisterAttached(
                "PlayerState",
                typeof(MediaPlayerState),
                typeof(Media),
                new PropertyMetadata(OnPlayerStateChanged)
                );

        public static void SetMediaSource(DependencyObject d, Uri uri)
        {
            d.SetValue(MediaSourceProperty, uri);
        }

        public static Uri GetMediaSource(DependencyObject d)
        {
            return d.GetValue(MediaSourceProperty) as Uri;
        }

        public static void SetPlayerState(DependencyObject d, MediaPlayerState playerState)
        {
            d.SetValue(PlayerStateProperty, playerState);
        }

        public static MediaPlayerState GetPlayerState(DependencyObject d)
        {
            return (MediaPlayerState)d.GetValue(PlayerStateProperty);
        }

        private static void OnMediaSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
                return;
            var mediaEl = d as MediaElement;
            if (mediaEl == null) return;

            mediaEl.Source = (e.NewValue as Uri);
            mediaEl.Play();
        }

        private static void OnPlayerStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
                return;

            var mediaEl = d as MediaElement;
            if (mediaEl == null) return;

            var playerState = (MediaPlayerState)e.NewValue;
            switch (playerState)
            {
                case MediaPlayerState.Paused:
                    mediaEl.Pause();
                    break;
                case MediaPlayerState.Playing:
                    mediaEl.Play();
                    break;
                case MediaPlayerState.Stopped:
                    mediaEl.Stop();
                    break;
            }
        }
    }
}
