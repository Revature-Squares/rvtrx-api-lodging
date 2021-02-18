using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.Context;
using RVTR.Lodging.Context.Repositories;
using RVTR.Lodging.Domain.Models;
using Xunit;
using System;

namespace RVTR.Lodging.Testing.Tests
{
  public class RepositoryTest : DataTest
  {
    public static readonly IEnumerable<object[]> Records = new List<object[]>
    {
      new object[]
      {
        new CampgroundModel { Id = 1, AddressId = 1, Bathrooms = 1, Name = "Test" },
        new CampsiteModel { Id = 1, LotNumber = "1", Price = 1.11, Status = "Available" },
        new ReviewModel { Id = 1, Comment = "Comment", DateCreated = DateTime.Now, Rating = 1, Name = "Bob" },
        new ImageModel { Id = 1, ImageUri = "" }
      }
    };

    [Theory]
    [MemberData(nameof(Records))]
    public async void Test_Repository_DeleteAsync(CampgroundModel campground, CampsiteModel campsite, ReviewModel review, ImageModel image)
    {
      using (var ctx = new CampgroundContext(Options))
      {
        ctx.Campsites.RemoveRange(ctx.Campsites);
        ctx.Campgrounds.RemoveRange(ctx.Campgrounds);
        ctx.Images.RemoveRange(ctx.Images);
        await ctx.Campsites.AddAsync(campsite);
        await ctx.Reviews.AddAsync(review);
        await ctx.Images.AddAsync(image);
        await ctx.Campgrounds.AddAsync(campground);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var campgrounds = new Repository<CampgroundModel>(ctx);

        await campgrounds.DeleteAsync(campground.Id);

        Assert.Equal(EntityState.Deleted, ctx.Entry(ctx.Campgrounds.Find(campground.Id)).State);
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var campsites = new Repository<CampsiteModel>(ctx);

        await campsites.DeleteAsync(campsite.Id);

        Assert.Equal(EntityState.Deleted, ctx.Entry(ctx.Campsites.Find(campsite.Id)).State);
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var reviews = new Repository<ReviewModel>(ctx);

        await reviews.DeleteAsync(review.Id);

        Assert.Equal(EntityState.Deleted, ctx.Entry(ctx.Reviews.Find(review.Id)).State);
      }
      using (var ctx = new CampgroundContext(Options))
      {
        var images = new Repository<ImageModel>(ctx);

        await images.DeleteAsync(image.Id);

        Assert.Equal(EntityState.Deleted, ctx.Entry(ctx.Images.Find(image.Id)).State);
      }
    }

    [Theory]
    [MemberData(nameof(Records))]
    public async void Test_Repository_InsertAsync(CampgroundModel campground, CampsiteModel campsite, ReviewModel review, ImageModel image)
    {
      using (var ctx = new CampgroundContext(Options))
      {
        ctx.Campsites.RemoveRange(ctx.Campsites);
        ctx.Campgrounds.RemoveRange(ctx.Campgrounds);
        ctx.Images.RemoveRange(ctx.Images);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var campgrounds = new Repository<CampgroundModel>(ctx);

        await campgrounds.InsertAsync(campground);

        Assert.Equal(EntityState.Added, ctx.Entry(campground).State);
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var campsites = new Repository<CampsiteModel>(ctx);

        await campsites.InsertAsync(campsite);

        Assert.Equal(EntityState.Added, ctx.Entry(campsite).State);
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var reviews = new Repository<ReviewModel>(ctx);

        await reviews.InsertAsync(review);

        Assert.Equal(EntityState.Added, ctx.Entry(review).State);
      }
      using (var ctx = new CampgroundContext(Options))
      {
        var images = new Repository<ImageModel>(ctx);

        await images.InsertAsync(image);

        Assert.Equal(EntityState.Added, ctx.Entry(image).State);
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync()
    {
      using (var ctx = new CampgroundContext(Options))
      {
        ctx.Campsites.RemoveRange(ctx.Campsites);
        ctx.Campgrounds.RemoveRange(ctx.Campgrounds);
        ctx.Images.RemoveRange(ctx.Images);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var campgrounds = new Repository<CampgroundModel>(ctx);

        var actual = await campgrounds.SelectAsync();

        Assert.Empty(actual);
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var campsites = new Repository<CampsiteModel>(ctx);

        var actual = await campsites.SelectAsync();

        Assert.Empty(actual);
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var reviews = new Repository<ReviewModel>(ctx);

        var actual = await reviews.SelectAsync();

        Assert.Empty(actual);
      }
      using (var ctx = new CampgroundContext(Options))
      {
        var images = new Repository<ImageModel>(ctx);

        var actual = await images.SelectAsync();

        Assert.Empty(actual);
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync_ById()
    {
      using (var ctx = new CampgroundContext(Options))
      {
        ctx.Campsites.RemoveRange(ctx.Campsites);
        ctx.Campgrounds.RemoveRange(ctx.Campgrounds);
        ctx.Images.RemoveRange(ctx.Images);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var campgrounds = new Repository<CampgroundModel>(ctx);

        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await campgrounds.SelectAsync(1));
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var campsites = new Repository<CampsiteModel>(ctx);

        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await campsites.SelectAsync(1));
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var reviews = new Repository<ReviewModel>(ctx);

        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await reviews.SelectAsync(1));
      }
      using (var ctx = new CampgroundContext(Options))
      {
        var images = new Repository<ImageModel>(ctx);

        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await images.SelectAsync(1));
      }
    }

    [Theory]
    [MemberData(nameof(Records))]
    public async void Test_Repository_Update(CampgroundModel campground, CampsiteModel campsite, ReviewModel review, ImageModel image)
    {
      using (var ctx = new CampgroundContext(Options))
      {
        ctx.Campsites.RemoveRange(ctx.Campsites);
        ctx.Campgrounds.RemoveRange(ctx.Campgrounds);
        ctx.Images.RemoveRange(ctx.Images);
        await ctx.Campgrounds.AddAsync(campground);
        await ctx.Campsites.AddAsync(campsite);
        await ctx.Reviews.AddAsync(review);
        await ctx.Images.AddAsync(image);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var campgrounds = new Repository<CampgroundModel>(ctx);
        var campgroundToUpdate = await ctx.Campgrounds.FirstAsync();

        campgroundToUpdate.Name = "Name";
        campgrounds.Update(campgroundToUpdate);

        var result = ctx.Campgrounds.Find(campground.Id);
        Assert.Equal(campgroundToUpdate.Name, result.Name);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var campsites = new Repository<CampsiteModel>(ctx);
        var campsiteToUpdate = await ctx.Campsites.FirstAsync();

        campsiteToUpdate.LotNumber = "4";
        campsites.Update(campsiteToUpdate);

        var result = ctx.Campsites.Find(campsite.Id);
        Assert.Equal(campsiteToUpdate.LotNumber, result.LotNumber);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var reviews = new Repository<ReviewModel>(ctx);
        var reviewToUpdate = await ctx.Reviews.FirstAsync();

        reviewToUpdate.Comment = "Comment";
        reviews.Update(reviewToUpdate);

        var result = ctx.Reviews.Find(review.Id);
        Assert.Equal(reviewToUpdate.Comment, result.Comment);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var images = new Repository<ImageModel>(ctx);
        var imageToUpdate = await ctx.Images.FirstAsync();

        imageToUpdate.ImageUri = "https://test.jpg";
        images.Update(imageToUpdate);

        var result = ctx.Images.Find(image.Id);
        Assert.Equal(imageToUpdate.ImageUri, result.ImageUri);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }
    }
  }
}
