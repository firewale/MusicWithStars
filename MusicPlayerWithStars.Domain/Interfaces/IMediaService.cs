using System;

namespace MusicPlayerWithStars.Domain
{
	public interface IMediaService
	{
		void Next();
		void Pause();
		void Play();
		void Previous();
		void SetOnSongDataRetrieved (Action<SongData> callback);
		void SetOnSongProgess (Action<double> callback);
	}
}

