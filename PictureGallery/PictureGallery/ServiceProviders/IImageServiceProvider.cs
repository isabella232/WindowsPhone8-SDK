using System;
using System.Collections.Generic;
using PictureGallery.ViewModels;

namespace PictureGallery.ServiceProviders
{
    /// <summary>
    /// Every image service provider should conform to this interface.
    /// An image service provider is an abstraction over an actual image service api (such as the Flickr API).
    /// </summary>
    public interface IImageServiceProvider
    {
        void SearchImages(string photoName, Action<PhotoListViewModel> onSearchComplete, int page = 1, int perPage = 12);

        void GetRecentImages(DateTime beginDate, Action<PhotoListViewModel> onSearchComplete, int page = 1, int perPage = 12);

        void GetImagesForTag(string tag, Action<PhotoListViewModel> onSearchComplete, int page = 1, int perPage = 12);

        void GetImagesForAuthor(string authorId, Action<PhotoListViewModel> onSearchComplete, int page = 1, int perPage = 12);

        void GetImagesForGallery(string galleryId, Action<PhotoListViewModel> onSearchComplete);

        PhotoListViewModel CreateTagPhotoList(string tag);

        PhotoListViewModel CreateAuthorPhotoList(string authorId);

        PhotoListViewModel CreateSearchPhotoList(string searchText);

        PhotoListViewModel CreateRecentPhotoList(DateTime startDate);

        TagListViewModel CreateRecentTagList();

        GalleryListViewModel CreateGalleryList(string authorId);

        void SearchAuthors(string authorName, Action<AuthorViewModel> onSearchComplete);

        void GetRecentTags(Action<IEnumerable<TagViewModel>> onSearchComplete);

        void GetAuthorOfPhoto(string photoId, Action<AuthorViewModel> onSearchComplete);

        void GetAuthor(string authorId, Action<AuthorViewModel> onSearchComplete);

        void GetComments(string photoId, Action<IEnumerable<CommentViewModel>> onSearchComplete);

        void GetGalleriesForAuthor(string authorId, Action<IEnumerable<GalleryViewModel>> onSearchComplete);

        bool HandleException(Exception ex, out string message);
    }
}