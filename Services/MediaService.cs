using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioRecord.Services
{
    public class MediaService
    {
        private MediaElement _mediaElement;
        public event EventHandler<MediaStateChangedEventArgs> StateChanged;

        public MediaElement MediaElement => _mediaElement;

        public MediaService()
        {
            _mediaElement = new MediaElement();
            _mediaElement.StateChanged += OnCurrentStateChanged;
        }
        private void OnCurrentStateChanged(object sender, MediaStateChangedEventArgs e)
        {
            StateChanged?.Invoke(this, e);
        }
        public void Play(Station item)
        {
            _mediaElement.MetadataTitle = item.title;
            _mediaElement.MetadataArtist = "RadioRecord";
            _mediaElement.MetadataArtworkUrl = item.icon_gray;
            _mediaElement.Source = MediaSource.FromUri(item.stream_hls);
            _mediaElement.Play();
        }

        public void Stop()
        {
            _mediaElement.Stop();
        }

        internal void EnsureMediaElementExists()
        {
            if (_mediaElement == null)
            {
                _mediaElement = new MediaElement();
            }
        }

        public bool IsPlaying => _mediaElement.CurrentState == MediaElementState.Playing;
    }
}
