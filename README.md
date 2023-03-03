# Background
MovieAPI is a RESTful service made as a .NET Backend Assignment. The aim is to use ASP.Net Core Web API to build a Movie Database that stores information about franchises, movies, and characters in each of the movies.

# Description
Using the Code first principle, the ASP.Net Core Web API creates a database called movieDbDTO with three tables: Characters, Movies, and Franchises, as well as a linking table between Movie and Character because of their many-to-many relationship. The database can then be manipulated using the REST Controllers CRUD calls.

Using Code first principle, the ASP.Net Core Web API creates a database called movieDbDTO which consist of 3 tables: Characters, Movies & Franchises as well as a linking table between Movie and Character because of their many to many relation. This Database can then be manipulated using the REST Controllers CRUD calls. 

<img width="584" alt="CRUD VideoDB" src="https://user-images.githubusercontent.com/44801529/222445611-61931dcf-a28b-4eb8-b5dd-0b5a26400e52.png">

# setting 
# Controllers
The MovieAPI has three controllers: Movie, Character, and Franchise.
## Movie Controller
The MovieController handles HTTP requests related to movies, including getting all movies, getting a single movie by ID, creating a new movie, updating an existing movie, and deleting a movie.

### Endpoints
- GetAllMovies: This method returns a list of all movies in the database. It doesn't require any parameters and will simply return all movies as a collection.

- GetMovieById: This method returns a specific movie resource based on its ID. It takes an ID parameter as input and will return the movie resource that matches the ID provided.

- PostMovie: This method creates a new movie resource. It takes a MovieCreateDto object as input, which contains the necessary information to create a new movie resource in the database.

- DeleteMovie: This method deletes a specific movie resource based on its ID. It takes an ID parameter as input and will delete the movie resource that matches the ID provided.

- PatchMovie: This method updates a specific movie resource with new data. It takes an ID parameter and a MovieUpdateDto object as input. The MovieUpdateDto object contains the new data to be updated for the movie resource.

- PatchMovieCharacters: This method updates the characters associated with a specific movie resource. It takes a MovieEditCharacterDto object as input, which contains a list of character IDs to be associated with the movie resource. This method updates the characters associated with the movie based on the movie ID provided in the MovieEditCharacterDto object.

## Character Controller
The CharacterController handles HTTP requests related to characters, including getting all characters, getting a single character by ID, creating a new character, updating an existing character, and deleting a character.


### Endpoints
- GetAllCharacters(): This method returns a list of all characters in the database. It doesn't require any parameters and will simply return all characters as a collection.

- GetCharacterById(int id): This method returns a specific character resource based on its ID. It takes an ID parameter as input and will return the character resource that matches the ID provided.

- PostCharacter(CreateCharacterDto createCharacterDto): This method creates a new character resource. It takes a CreateCharacterDto object as input, which contains the necessary information to create a new character resource in the database.

- DeleteCharacter(int id): This method deletes a specific character resource based on its ID. It takes an ID parameter as input and will delete the character resource that matches the ID provided.

- PatchCharacter(int id, CharacterEditDto CharacterEditDto): This method updates a specific character resource with new data. It takes an ID parameter and a CharacterEditDto object as input. The CharacterEditDto object contains the new data to be updated for the character resource.

## Franchise Controller
The FranchiseController handles HTTP requests related to franchises, including getting all franchises, getting a single franchise by ID, creating a new franchise, updating an existing franchise, and deleting a franchise.

### Endpoints
- GetAllFranchises(): This method returns all the franchises that are stored in the database. The method retrieves the franchises from the database and returns them as a list of FranchiseDto objects. Each FranchiseDto object contains information about the franchise, such as the name, description, and ID.

- GetFranchiseById(int id): This method retrieves a single franchise from the database by its ID. It takes an integer parameter (id) that represents the ID of the franchise to be retrieved. If a franchise with the given ID exists in the database, the method returns a FranchiseDto object containing the franchise's information. If no franchise with the given ID exists in the database, the method returns null.

- GetAllIdFranchiseCharacters(int id): This method retrieves all the movie characters associated with a particular franchise. It takes an integer parameter (id) that represents the ID of the franchise for which the movie characters are to be retrieved. The method retrieves the movie characters from the database and returns them as a list of MovieCharacterDto objects. Each MovieCharacterDto object contains information about the movie character, such as the name, description, and ID.

- PostFranchise(CreateFranchiseDto createFranchiseDto): This method creates a new franchise in the database. It takes a CreateFranchiseDto object as a parameter, which contains information about the new franchise to be created, such as the name and description. The method adds the new franchise to the database and returns a FranchiseDto object containing the information about the newly created franchise, including the newly assigned ID.

- DeleteFranchise(int id): This method deletes a franchise from the database by its ID. It takes an integer parameter (id) that represents the ID of the franchise to be deleted. If a franchise with the given ID exists in the database, the method deletes it from the database and returns true. If no franchise with the given ID exists in the database, the method returns false.

- PatchFranchise(int id, FranchiseEditDto franchiseEditDto): This method updates an existing franchise in the database with new data. It takes two parameters: an integer parameter (id) that represents the ID of the franchise to be updated, and a FranchiseEditDto object that contains the updated information for the franchise. The method retrieves the franchise from the database, updates its information with the new data, and then saves the updated franchise back to the database. It returns a FranchiseDto object containing the updated information for the franchise.

- PatchFranchiseMovies(int id, FranchiseEditMovieDto franchiseEditMovieDto): This method updates all the movies associated with a particular franchise in the database. It takes two parameters: an integer parameter (id) that represents the ID of the franchise whose movies are to be updated, and a FranchiseEditMovieDto object that contains the updated information for the movies. The method retrieves all the movies associated with the given franchise ID from the database, updates their information with the new data, and then saves the updated movies back to the database. The FranchiseEditMovieDto object contains a list of MovieEditDto objects, each of which represents the updated information for a movie associated with the franchise. The method returns a list of MovieDto objects containing the updated information for all the movies associated with the franchise.
