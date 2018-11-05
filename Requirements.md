# Software Requirements

## Vision

### What is the vision of this product?

This web application is designed to provide people with more insights and awareness concerning local pet adoptions from user generated content.

### What pain point does this project solve?

When a person seeks to adopt a pet, he/she would generally like to know as much as possible about the animal before adopting. 
This application seeks to provide more ease in the process of pet adoption from an informational POV, allowing people to see feedback
from other users who might have already interacted with the specific animal in question.

### Why should we care about your product?

Users who seek to adopt pets would find this application useful in assessing the feasibility of adopting a pet based on their requirments.

## Scope (In/Out)

### IN - What will your product do

This application connects with the PETFINDER API to allow users to see animals available for adoption in his/her local area.

The user can add an animal to interest to a "Favorites" list to track and keep tabs on the animal.

The user can view the profile of each animal to see any existing user feedback to assess the possibility of adopting the animal.

If there are no user reviews, the user can add a review himself/herself should they visit the pet in order to obtain an initial impression
to allow other users to assess the adoption of animals.

### OUT

This web application is informational only, and will not facilitate the adoption of the animal


## MVP

Custom Made API with 2 Endpoints. The first Endpoint will return all user reviews. The second Endpoint will return the profile of every pet that has a review.
The Web Application will interact with the API for CRUD operations.
The User will be able to create and account that will be stored in a database and be able to login.
Profile Page will should detailed information of Pet including user review.
Favorites Page will show all Pets user added to the collection.
Home Page will display Pets for user to select.

## Stretch

Utilize BING MAPS API to allow directions to the animal shelter.
Social Media sharing of a specific pet.
A smooth and clean looking UI as possible.

## Functional Requirements

A User can search for pets in his/her area
A User can view the profile of any pet
A User can add and edit his/her review of the pet
A User can add any pet to his/her Favorites Page.

## Non-Functional Requirements

The code generated should be testable: 
	API should pass all CRUD operations test to ensure correct data flow.
	Database should pass all CRUD operations test to ensure correct data flow.
HTML/CSS stylings to provide clean looking website for User Experience.


## Data Flow

The User will create an account, the user profile information will be added to the database.
The Front Page will make API GET request to Petfinder to render all pets the user specified in the Search Form (POST).
Once the User selects a PET, the PET profile page will make API GET requests (using the specific PETID) to both the PETFINDER and CUSTOM API to render the Pet's data and user reviews.
Once the User adds a Pet to Favorites, the PETID information along with USERID will be added to the database and will be retrieved to render that PET on the Favorites Page.
The User can create a review of the pet and the web application will create a POST request to the CUSTOM API to store in the API's database using USERID and PETID
The User can edit a review and the web application will create a PUT request to CUSTOM API.
The User can delete a review to generate a DELETE request.
The Favorites Page will retrieved PET data from the web application database based on USERID.





