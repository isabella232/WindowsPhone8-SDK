using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using FlickrNet;
using FlickrNet.Exceptions;
using PictureGallery.ViewModels;
using Telerik.Windows.Controls;

namespace PictureGallery.ServiceProviders
{
    public class FlickrServiceProvider : IImageServiceProvider
    {
        private readonly Flickr flickr = new Flickr("your-own-api-key");

        // Matches html tags so that they can be removed from the comments for each photo.
        // For more information see this http://stackoverflow.com/questions/787932/using-c-sharp-regular-expressions-to-remove-html-tags.
        private readonly Regex htmlTagFinder = new Regex("<(.|\n)+?>");

        // Matches emails when searching for users. For more information see this.
        // http://msdn.microsoft.com/en-us/library/01escwtf.aspx
        private readonly Regex mailVerifier = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");

        private readonly char whitespace = ' ';

        /// <summary>
        /// Searches the titles of the images for the specified name.
        /// </summary>
        /// <param name="photoName">The name of the photo to search for.</param>
        /// <param name="onSearchComplete">A callback that will be called once the photos are downloaded.</param>
        public void SearchImages(string photoName, Action<PhotoListViewModel> onSearchComplete, int page = 1, int perPage = 12)
        {
            PhotoSearchOptions options = new PhotoSearchOptions(string.Empty, string.Empty, TagMode.None, photoName);
            options.PerPage = perPage;
            options.Page = page;
            this.flickr.PhotosSearchAsync(options, (photos) =>
            {
                if (photos.HasError)
                {
                    return;
                }

                onSearchComplete(this.CreatePhotoList(photos.Result, new SearchPhotoDownloader() { Text = photoName }));
            });
        }

        /// <summary>
        /// Gets the photos of an author.
        /// </summary>
        /// <param name="authorId">The author id of the author for which to get photos.</param>
        /// <param name="onSearchComplete">A callback that will be called once the photos are downloaded.</param>
        public void GetImagesForAuthor(string authorId, Action<PhotoListViewModel> onSearchComplete, int page = 1, int perPage = 12)
        {
            flickr.PeopleGetPublicPhotosAsync(authorId, page, perPage, (result) =>
            {
                if (result.HasError)
                {
                    return;
                }

                onSearchComplete(this.CreatePhotoList(result.Result, new AuthorPhotoDownloader() { AuthorId = authorId }));
            });
        }

        /// <summary>
        /// Gets the photos that are tagged with the provided tag.
        /// </summary>
        /// <param name="tag">The tag with which the returned photos will be tagged.</param>
        /// <param name="onSearchComplete">A callback that will be called once the photos are downloaded.</param>
        /// <param name="page">The page number to download.</param>
        public void GetImagesForTag(string tag, Action<PhotoListViewModel> onSearchComplete, int page = 1, int perPage = 12)
        {
            PhotoSearchOptions options = new PhotoSearchOptions(string.Empty, tag);
            options.Page = page;
            options.PerPage = perPage;
            flickr.PhotosSearchAsync(options, (photos) =>
            {
                if (photos.HasError)
                {
                    return;
                }

                onSearchComplete(this.CreatePhotoList(photos.Result, new TagPhotoDownloader() { Tag = tag }));
            });
        }

        /// <summary>
        /// Gets uploaded images after the specified begin date.
        /// </summary>
        /// <param name="beginDate">The date after which the required photos are uploaded.</param>
        /// <param name="onSearchComplete">A callback that is invoked when the photos are downloaded.</param>
        /// <param name="refresh">Get the photos from the network instead of the local cache.</param>
        public void GetRecentImages(DateTime beginDate, Action<PhotoListViewModel> onSearchComplete, int page = 1, int perPage = 12)
        {
            flickr.InterestingnessGetListAsync(beginDate, PhotoSearchExtras.All, page, perPage, (photos) =>
            {
                // This will happen if the phone is connected to a local network but not to the internet or if it does
                // not have access to flickr for some reason.
                if (photos.HasError)
                {
                    return;
                }

                PhotoListViewModel photoList = this.CreatePhotoList(photos.Result, new InterestingPhotoDownloader() { BeginDate = beginDate });
                onSearchComplete(photoList);
            });
        }
        
        /// <summary>
        /// Gets all images in the specified gallery.
        /// </summary>
        /// <param name="galleryId">The id of the gallery.</param>
        /// <param name="onSearchComplete">A callback that is invoked when the gallery photos are downloaded.</param>
        public void GetImagesForGallery(string galleryId, Action<PhotoListViewModel> onSearchComplete)
        {
            this.flickr.GalleriesGetPhotosAsync(galleryId, (result) =>
            {
                if (result.HasError)
                {
                    return;
                }

                onSearchComplete(this.CreatePhotoList(result.Result, new GalleryPhotoDownloader() { GalleryId = galleryId }));
            });
        }

        public PhotoListViewModel CreateTagPhotoList(string tag)
        {
            PhotoListViewModel list = new PhotoListViewModel();
            list.PhotoDownloader = new TagPhotoDownloader() { Tag = tag };

            return list;
        }

        public PhotoListViewModel CreateAuthorPhotoList(string authorId)
        {
            PhotoListViewModel list = new PhotoListViewModel();
            list.PhotoDownloader = new AuthorPhotoDownloader() { AuthorId = authorId };

            return list;
        }

        public GalleryListViewModel CreateGalleryList(string authorId)
        {
            GalleryListViewModel list = new GalleryListViewModel();
            list.AuthorId = authorId;

            return list;
        }

        public PhotoListViewModel CreateSearchPhotoList(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return null;
            }

            PhotoListViewModel list = new PhotoListViewModel();
            list.PhotoDownloader = new SearchPhotoDownloader() { Text = searchText };

            return list;
        }

        public PhotoListViewModel CreateRecentPhotoList(DateTime startDate)
        {
            PhotoListViewModel list = new PhotoListViewModel();
            list.PhotoDownloader = new InterestingPhotoDownloader() { BeginDate = startDate };

            return list;
        }

        public TagListViewModel CreateRecentTagList()
        {
            return new TagListViewModel();
        }

        /// <summary>
        /// Gets the recently created tags.
        /// </summary>
        /// <param name="onSearchComplete">A callback that is invoked when the latest tags are downloaded.</param>
        /// <param name="refresh">Get the tags from the network instead of the local cache.</param>
        public void GetRecentTags(Action<IEnumerable<TagViewModel>> onSearchComplete)
        {
            flickr.TagsGetHotListAsync((tags) =>
                {
                    if (tags.HasError)
                    {
                        return;
                    }

                    IEnumerable<TagViewModel> hotTags = from tag in tags.Result select this.CreateTagViewModel(tag);
                    onSearchComplete(hotTags);
                });
        }

        /// <summary>
        /// Gets the galleries of the provided author.
        /// </summary>
        /// <param name="authorId">The author of the galleries.</param>
        /// <param name="onSearchComplete">A callback that will be called once the galleries are downloaded.</param>
        public void GetGalleriesForAuthor(string authorId, Action<IEnumerable<GalleryViewModel>> onSearchComplete)
        {
            flickr.GalleriesGetListAsync(authorId, (result) =>
            {
                if (result.HasError)
                {
                    return;
                }

                IEnumerable<GalleryViewModel> galleries = from gallery in result.Result select this.CreateGalleryViewModel(gallery);
                onSearchComplete(galleries);
            });
        }

        /// <summary>
        /// Gets the author of a photo.
        /// </summary>
        /// <param name="photoId">The photo id of the photo for which to get comments.</param>
        /// <param name="onSearchComplete">A callback that will be called once the author is downloaded.</param>
        public void GetAuthorOfPhoto(string photoId, Action<AuthorViewModel> onSearchComplete)
        {
            flickr.PhotosGetInfoAsync(photoId, (result) =>
            {
                if (result.HasError)
                {
                    return;
                }

                flickr.PeopleGetInfoAsync(result.Result.OwnerUserId, (person) =>
                    {
                        if (person.HasError)
                        {
                            return;
                        }

                        AuthorViewModel author = this.CreateAuthorViewModel(person.Result);
                        onSearchComplete(author);
                    });
            });
        }

        /// <summary>
        /// Gets the comments for a photo.
        /// </summary>
        /// <param name="photoId">The photo id of the photo for which to get comments.</param>
        /// <param name="onSearchComplete">A callback that will be called once the comments are downloaded.</param>
        public void GetComments(string photoId, Action<IEnumerable<CommentViewModel>> onSearchComplete)
        {
            this.flickr.PhotosCommentsGetListAsync(photoId, (result) =>
                {
                    if (result.HasError)
                    {
                        return;
                    }

                    Collection<CommentViewModel> comments = new Collection<CommentViewModel>();
                    result.Result.Apply<PhotoComment>((photoComment) => comments.Add(this.CreateCommentViewModel(photoComment)));
                    onSearchComplete(comments);
                });
        }

        /// <summary>
        /// Searches for an author with the specified user name.
        /// </summary>
        /// <param name="authorName">The user name of the author to search for.</param>
        /// <param name="onSearchComplete">A callback that is invoked once the author is downloaded</param>
        public void SearchAuthors(string authorName, Action<AuthorViewModel> onSearchComplete)
        {
            if (this.mailVerifier.IsMatch(authorName))
            {
                this.SearchUserByMail(authorName, onSearchComplete);
            }
            else
            {
                this.SearchUserByUsername(authorName, onSearchComplete);
            }
        }

        /// <summary>
        /// Gets the author for the specified author id.
        /// </summary>
        /// <param name="authorId">The id of the author to get.</param>
        /// <param name="onSearchComplete">Called when the author is downloaded.</param>
        public void GetAuthor(string authorId, Action<AuthorViewModel> onSearchComplete)
        {
            this.flickr.PeopleGetInfoAsync(authorId, (result) =>
            {
                if (result.HasError)
                {
                    return;
                }

                onSearchComplete(this.CreateAuthorViewModel(result.Result));
            });
        }

        /// <summary>
        /// Called by the application to handle exceptions from the image service.
        /// </summary>
        /// <param name="ex">The exception to handle.</param>
        /// <param name="message">An out message reference that will contain some meaningful explanation of the image service exception (if the exception is handled) when this method returns.</param>
        /// <returns>Returns true or false if the ImageServiceProvider has handled the exception or not.</returns>
        public bool HandleException(Exception ex, out string message)
        {
            message = null;

            if (ex is UserNotFoundException)
            {
                message = "No user found with the specified user name or email.";
                return true;
            }

            if (ex is PhotoNotFoundException)
            {
                message = "This photo no longer exists.";
                return true;
            }

            if (ex is FlickrApiException)
            {
                message = ex.Message;
                return true;
            }

            return false;
        }
        
        private AuthorViewModel CreateAuthorViewModel(Person author)
        {
            int photoCount = 0;
            if (author.PhotosSummary != null)
            {
                photoCount = author.PhotosSummary.PhotoCount;
            }

            return new AuthorViewModel(author.UserId, author.UserName, author.BuddyIconUrl, photoCount);
        }

        private GalleryViewModel CreateGalleryViewModel(Gallery gallery)
        {
            return new GalleryViewModel(gallery.GalleryId, gallery.Title, gallery.Description, gallery.OwnerId);
        }

        private TagViewModel CreateTagViewModel(HotTag tag)
        {
            return new TagViewModel(tag.Tag, tag.Score);
        }

        private CommentViewModel CreateCommentViewModel(PhotoComment comment)
        {
            return new CommentViewModel(comment.AuthorUserName, comment.AuthorUserId, comment.AuthorBuddyIcon, this.RemoveHtmlTags(comment.CommentHtml), comment.DateCreated);
        }
        
        private PhotoViewModel CreatePhotoViewModel(Photo pic)
        {
            List<string> list = new List<string> { pic.SmallUrl, pic.MediumUrl, pic.LargeUrl };
            Collection<string> sizes = new Collection<string>(list);
            Collection<string> tags = new Collection<string>(pic.Tags);

            return new PhotoViewModel(pic.Title, pic.PhotoId, pic.UserId, pic.ThumbnailUrl, pic.OriginalUrl, sizes, tags);
        }

        private string RemoveHtmlTags(string html)
        {
            string result = htmlTagFinder.Replace(html, (match) => string.Empty).TrimStart(this.whitespace);
            return result.TrimEnd(this.whitespace);
        }

        private void SearchUserByMail(string email, Action<AuthorViewModel> onSearchComplete)
        {
            this.flickr.PeopleFindByEmailAsync(email, (foundUser) =>
            {
                if (foundUser.Error != null)
                {
                    return;
                }

                this.GetUser(foundUser.Result.UserId, onSearchComplete);
            });
        }
        private void SearchUserByUsername(string username, Action<AuthorViewModel> onSearchComplete)
        {
            this.flickr.PeopleFindByUserNameAsync(username, (foundUser) =>
            {
                if (foundUser.Error != null)
                {
                    return;
                }

                this.GetUser(foundUser.Result.UserId, onSearchComplete);
            });
        }

        private void GetUser(string userId, Action<AuthorViewModel> onSearchComplete)
        {
            this.flickr.PeopleGetInfoAsync(userId, (person) =>
            {
                AuthorViewModel author = this.CreateAuthorViewModel(person.Result);
                onSearchComplete(author);
            });
        }

        private PhotoListViewModel CreatePhotoList(PhotoCollection photos, IPhotoDownloader downloader)
        {
            PhotoListViewModel result = this.CreatePhotoListCore(from pic in photos select this.CreatePhotoViewModel(pic), downloader);
            result.TotalPhotos = photos.Total;
            return result;
        }

        private PhotoListViewModel CreatePhotoList(GalleryPhotoCollection photos, IPhotoDownloader downloader)
        {
            PhotoListViewModel result = this.CreatePhotoListCore(from pic in photos select this.CreatePhotoViewModel(pic), downloader);
            result.TotalPhotos = photos.Total;
            return result;
        }

        private PhotoListViewModel CreatePhotoListCore(IEnumerable<PhotoViewModel> photos, IPhotoDownloader downloader)
        {
            PhotoListViewModel photoList = new PhotoListViewModel();
            photoList.Photos = new ObservableCollection<PhotoViewModel>(photos);
            photoList.LoadedPages = 1;
            photoList.PhotoDownloader = downloader;

            return photoList;
        }
    }
}
