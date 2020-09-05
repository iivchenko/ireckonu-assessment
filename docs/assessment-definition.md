# Assignment context resolution

The assignment doesn’t provide context and business cases and the file with test data doesn’t have description. Next questions should be answered:

* What is the source of the file? Is it third party clients that can provide different formats for the file or is it only Ireckonu stakeholders that have predefined formats?
* What information the test file represents?
* What actions the file represents? Is it insertions of a new items? Could it contain update for already existing items in a database?
* What is the KEY field representing? Is it a random and unique ID? Is it a combination of existing properties?
* Fields like ArtikelCode, ColorCode, Price, DiscountPrice, Size - do they represents regional or country formats or is it some generic and standard format?
        
The assessment was made **during weekends (05 and 06 of September)** so the answers I gave by myself:

* The file provided only by our stakeholders
* The file has standardized and predictable structure
* The domain is a retail product definition
* The file represents new products to be added to the database or/and old products to be updated
* The Key field represents a random unique ID. If product with ID is missing in the database - new product added, if product with ID already exists in the database - product updated.
* ArtikelCode, ColorCode fields represent values defined inside of the company
* Price, DiscountPrice fields standartized to use EUR currency only
* Size field standartized to use European size only