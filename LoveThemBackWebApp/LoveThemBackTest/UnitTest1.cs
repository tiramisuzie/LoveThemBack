using LoveThemBackWebApp.Data;
using LoveThemBackWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace LoveThemBackTest
{
    public class UnitTest1
    {
        /// <summary>
        /// Tests Favorite notes
        /// </summary>
        [Fact]
        public void CanGetNotesFromFavoriteModelTest()
        {
            Favorite fav = new Favorite();
            fav.Notes = "This is a note.";

            Assert.Equal("This is a note.", fav.Notes);
        }

        /// <summary>
        /// Tests the ability to change notes on favorites
        /// </summary>
        [Fact]
        public void CanUpdateNotesOnFavoriteTest()
        {
            Favorite fav = new Favorite();
            fav.Notes = "blahblah";

            fav.Notes = "blahblahblah";

            Assert.Equal("blahblahblah", fav.Notes);
        }


        /// <summary>
        /// Tests can set review on animal
        /// </summary>
        [Fact]
        public void CanGetReviewTest()
        {
            Reviews rev = new Reviews();
            rev.Impression = "Good boy";

            Assert.Equal("Good boy", rev.Impression);
        }

        /// <summary>
        /// Tests can change review on animal
        /// </summary>
        [Fact]
        public void CanChangeReviewTest()
        {
            Reviews rev = new Reviews();
            rev.Impression = "Good boy";

            rev.Impression = "Good girl";

            Assert.Equal("Good girl", rev.Impression);
        }

        /// <summary>
        /// Tests can change review on animal
        /// </summary>
        [Fact]
        public void CanReturBoolPropertyOnReviewsTest()
        {
            Reviews rev = new Reviews();
            rev.Playful = true;

            Assert.True(rev.Playful);
        }

        /// <summary>
        /// Tests can change review on animal
        /// </summary>
        [Fact]
        public void CanChangeBoolPropertyReviewTest()
        {
            Reviews rev = new Reviews();
            rev.Healthy = true;

            rev.Healthy = false;

            Assert.False(rev.Healthy);
        }

        /// <summary>
        /// Tests Favorite notes
        /// </summary>
        [Fact]
        public void CanCreateProfileAndReturnUsernameTest()
        {
            Profile profile = new Profile();
            profile.Username = "Banul Beleon";

            Assert.Equal("Banul Beleon", profile.Username);
        }


        /// <summary>
        /// Tests Favorite notes
        /// </summary>
        [Fact]
        public void CanChangeUsernameOnProfileTest()
        {
            Profile profile = new Profile();
            profile.Username = "Banul Beleon";

            profile.Username = "Boozie Bu";

            Assert.Equal("Boozie Bu", profile.Username);
        }

        [Fact]
        public async void CRUDProfileTest()
        {
            DbContextOptions<LTBDBContext> options =
                new DbContextOptionsBuilder<LTBDBContext>()
                .UseInMemoryDatabase("Profile")
                .Options;

            using (LTBDBContext context = new LTBDBContext(options))
            {

                //CREATE
                Profile profile = new Profile();
                profile.Username = "Greg";
                profile.LocationZip = 98107;

                context.Profiles.Add(profile);
                context.SaveChanges();

                //READ
                var newProfile = await context.Profiles.FirstOrDefaultAsync(r => r.Username == profile.Username);
                Assert.Equal("Greg", newProfile.Username);

                //UPDATE
                newProfile.Username = "Carl";
                context.Update(newProfile);
                context.SaveChanges();

                var newProf = await context.Profiles.FirstOrDefaultAsync(r => r.Username == newProfile.Username);
                Assert.Equal("Carl", newProf.Username);

                //DELETE
                context.Profiles.Remove(newProf);
                context.SaveChanges();

                var deletedProfile = await context.Profiles.FirstOrDefaultAsync(r => r.Username == newProf.Username);
                Assert.True(deletedProfile == null);
            }
        }

            [Fact]
        public async void CRUDFavoritesTest()
        {
            DbContextOptions<LTBDBContext> options =
                new DbContextOptionsBuilder<LTBDBContext>()
                .UseInMemoryDatabase("Favorites")
                .Options;

            using (LTBDBContext context = new LTBDBContext(options))
            {

                //CREATE
                Favorite fav = new Favorite();
                Profile profile = new Profile();
                profile.Username = "Greg";
                profile.LocationZip = 98107;
                fav.Profile = profile;
                fav.Notes = "G'BOY GREGG";

                context.Favorites.Add(fav);
                context.SaveChanges();

                //READ
                var newFav = await context.Favorites.FirstOrDefaultAsync(r => r.Profile == fav.Profile);
                Assert.Equal("Greg", fav.Profile.Username);

                //UPDATE
                newFav.Notes = "BAD BOY GREG! You're Bad!";
                context.Update(newFav);
                context.SaveChanges();

                var newFavorite = await context.Favorites.FirstOrDefaultAsync(r => r.Notes == newFav.Notes);
                Assert.Equal("BAD BOY GREG! You're Bad!", newFavorite.Notes);

                //DELETE
                context.Favorites.Remove(newFavorite);
                context.SaveChanges();

                var deletedFavorite = await context.Favorites.FirstOrDefaultAsync(r => r.Profile.Username == newFavorite.Profile.Username);
                Assert.True(deletedFavorite == null);
            }
        }
    }
}
