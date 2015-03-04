using System;
using System.Collections.Generic;
using PictureGallery.ViewModels;

namespace PictureGallery.ServiceProviders
{
    /// <summary>
    /// This provider is used when the app has no network access, since it is easier to do nothing instead of
    /// check for network access in every single method.
    /// </summary>
    public class DummyServiceProvider : IImageServiceProvider
    {
        public void SearchImages(string photoName, Action<PhotoListViewModel> onSearchComplete, int page = 1, int perPage = 12)
        {
        }

        public void GetRecentImages(DateTime beginDate, Action<PhotoListViewModel> onSearchComplete, int page = 1, int perPage = 12)
        {
        }

        public void GetImagesForTag(string tag, Action<PhotoListViewModel> onSearchComplete, int page = 1, int perPage = 12)
        {
        }

        public void GetImagesForAuthor(string authorId, Action<PhotoListViewModel> onSearchComplete, int page = 1, int perPage = 12)
        {
        }

        public void GetImagesForGallery(string galleryId, Action<PhotoListViewModel> onSearchComplete)
        {
        }

        public TagListViewModel CreateRecentTagList()
        {
            return null;
        }

        public PhotoListViewModel CreateTagPhotoList(string tag)
        {
            return null;
        }

        public PhotoListViewModel CreateAuthorPhotoList(string authorId)
        {
            return null;
        }

        public GalleryListViewModel CreateGalleryList(string authorId)
        {
            return null;
        }

        public PhotoListViewModel CreateSearchPhotoList(string searchText)
        {
            return null;
        }

        public PhotoListViewModel CreateRecentPhotoList(DateTime startDate)
        {
            return null;
        }

        public void SearchAuthors(string authorName, Action<AuthorViewModel> onSearchComplete)
        {
        }

        public void GetRecentTags(Action<IEnumerable<TagViewModel>> onSearchComplete)
        {
        }

        public void GetAuthorOfPhoto(string photoId, Action<AuthorViewModel> onSearchComplete)
        {
        }

        public void GetComments(string photoId, Action<IEnumerable<CommentViewModel>> onSearchComplete)
        {
        }

        public void GetGalleriesForAuthor(string authorId, Action<IEnumerable<GalleryViewModel>> onSearchComplete)
        {
        }

        public bool HandleException(Exception ex, out string message)
        {
            message = null;
            return false;
        }

        public void GetAuthor(string authorId, Action<AuthorViewModel> onSearchComplete)
        {
        }
    }
}