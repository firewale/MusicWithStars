using System;
using System.Runtime.Serialization;

namespace MusicPlayerWithStars.Domain
{
    [DataContract()]
	public class SongData : IEquatable<SongData>
	{
		public SongData (string title, string album, string artist, string genre, double duration)
		{
			Title = title;
			Album = album;
			Artist = artist;
			Genre = genre;
			Duration = duration;
		}

        [DataMember()]
		public string Title { get; private set; }

        [DataMember()]
        public string Album { get; private set; }

        [DataMember()]
        public string Artist { get; private set; }

        [DataMember()]
        public string Genre { get; private set; }

        [DataMember()]
        public double Duration { get; private set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as SongData);
        }

        public bool Equals(SongData other)
        {
            if (ReferenceEquals(other, null)) return false;

            if (string.Compare(Title, other.Title) != 0) return false;
            if (string.Compare(Album, other.Album) != 0) return false;
            if (string.Compare(Artist, other.Artist) != 0) return false;
            if (string.Compare(Genre, other.Genre) != 0) return false;

            return Duration == other.Duration;
        }

        public override int GetHashCode()
        {
            int hashcode = 0;

            if (Title != null)
                hashcode ^= Title.GetHashCode();

            if (Album != null)
                hashcode ^= Album.GetHashCode();

            if (Artist != null)
                hashcode ^= Artist.GetHashCode();

            if (Genre != null)
                hashcode ^= Genre.GetHashCode();

            return hashcode;
        }
    }
}

