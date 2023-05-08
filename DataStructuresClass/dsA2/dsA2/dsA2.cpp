/*
 * Filename: dsA2
 * By: Justin Fink
 * Date: Firday 09 April 2021
 * Description: This program takes a file with over 2000 names as input 
 *				these names are then put into both a Long Linked list and 
 *				a Hash Table with 127 buckets. The user then inputs a name
				to search for and then a couple functions go through both lists
				to figure out if the name is in them, this program shows the effiency
 *				increase in the Hash Table since it has on occasion 10 times less
 *				to search for. At the end it prints off the total number of searches and
 *				comparisons and then frees all memory and closes the file.
 * 
 */



#include <stdio.h>
#include <malloc.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#pragma warning(disable:4996)



 //Constants
#define STRING_LENGTH 21
#define TABLE_LENGTH 127



//Prototypes
int myHashFunction(char* word);
unsigned long hash(const char* str);
int bucket(unsigned long hash);
struct NameNode* searchLinkedList(char* searchName, struct NameNode* head, int* comparisonCount);
void searchForNameTwice(char* searchName, struct NameNode* linkedList, struct NameNode* hashTable, int comparisonCount[2]);
void freeMemory(NameNode** head);
struct NameNode* getWordInfo(struct NameNode** newHead, char* word);
const char* nTakeaway(char* str);


//Structure
struct NameNode
{
	char name[STRING_LENGTH] = {};
	struct NameNode* next;
};



int main()
{
	int periodCheck = 1;
	int bucketNumber = 0;
	int counter = 0;
	char name[STRING_LENGTH] = {};

	int numberOfSearches = 0;
	int comparisonCount[2] = { 0 };

	
	struct NameNode* hashTable[TABLE_LENGTH] = {};
	//Set all of the buckets to NULL
	for (counter = 0; counter < TABLE_LENGTH; counter++)
	{
		hashTable[counter] = NULL;
	}
	
	struct NameNode* bigList = NULL;


	//Setting up the file
	FILE* pInputFile = NULL;

	pInputFile = fopen("names.txt", "r");
	if (pInputFile == NULL)
	{
		printf("File I/O error");
		return 0;
	}




	//Loop for putting a names from a file into a hast table and long
	//sorted linked list
	while (fgets(name, STRING_LENGTH, pInputFile) != NULL)
	{
		
		nTakeaway(name);

		//Get the correct bucket number for the name
		bucketNumber = myHashFunction(name);
		//We now have the bucket number

		//Inserting the word into the bukcet as well as LONG linked list
		getWordInfo(&hashTable[bucketNumber], name);
		getWordInfo(&bigList, name);
	}

	//Prompt User
	printf("Search for names.  Input '.' to exit the loop.\n\n");

	//Loop for searching in the Hash Table and Long Linked List
	while (periodCheck != 0)
	{
		//promt user to search for a word
		fgets(name, STRING_LENGTH, stdin);
		nTakeaway(name);

		periodCheck = strcmp(name, ".");

		if (periodCheck != 0)
		{
			//Get the correct bucket number for the searched name
			bucketNumber = myHashFunction(name); //have the bucket number

	
			searchForNameTwice(name, bigList, hashTable[bucketNumber], comparisonCount);
			numberOfSearches++;
		}
	}



	
	printf("\n");
	printf("Total Number of Searches: %d\n", numberOfSearches);
	printf("Total Number of Comparisons in Linked List: %d\n", comparisonCount[0]);
	printf("Total Number of Comparisons in Hash Table: %d\n", comparisonCount[1]);


	//FREE THE MEMORY
	for (counter = 0; counter < TABLE_LENGTH; counter++)
	{
		freeMemory(&hashTable[counter]);
	}
	freeMemory(&bigList);

	//CLOSE THE FILE
	fclose(pInputFile);


	return 0;
}




/*
 * Function: myHashFunction()
 * Description: This function takes a name and puts it into a hash function then it puts it into a bucket function
 *				to then get the correct bucket number for that specific word
 * Parameters: char* word
 * Return Values: It returns the bucket number te word should go into
 */
int myHashFunction(char* name)
{
	unsigned long hashValue = 0;
	int bucketNumber = 0;
	hashValue = hash(name); //gets the hash value
	bucketNumber = bucket(hashValue); // gets the bucket number
	return bucketNumber;
}





/*
 * This function was found online at http://www.cse.yorku.ca/~oz/hash.html and is the djb2 hash algoritm
 * made by Dan Bernstein, NOT by me
 *
 * Function: hash() or the djb2 function from dan bernstein
 * Description: This function returns a hash value with the word inputted
 * Parameters: Const char* str (a string)
 * Return Values: It returns the hash value
 */
unsigned long hash(const char* str)
{
	unsigned long hash = 5381;
	int c = 0;
	while ((c = *str++) != '\0')
	{
		hash = ((hash << 5) + hash) + c;
	}
	return hash;
}




/*
 * Function: bucket()
 * Description: This function does the division method with the has number
 * Parameters: Unsigned long hash
 * Return Values: It returns which bucket number the word should go into
 */
int bucket(unsigned long hash)
{
	int bucketNumber = 0;
	bucketNumber = hash % TABLE_LENGTH;
	return bucketNumber;
}




/*
 * Function: searchLinkedList()
 * Description: This function searches a sorted linked list for a certain name, this function 
 *				searches until it passes the point in which the name should have been founnd if it 
 *				is not there, it also keeps track of how many comparisons were made
 * Parameters: Char* searchName					the name to look for
 *			   struct NameNode* linkedList		the linked list to search
 *			   int* comparisonCount				the comparison count
 * Return Values: This function return anything
 */
struct NameNode* searchLinkedList(char* searchName, struct NameNode* linkedList, int* comparisonCount)
{
	NameNode* sortedList = linkedList;
	bool success = false;
	int comparisons = 0;

	//Go through the list
	while (sortedList != NULL)
	{
		comparisons++;
		
		//If you found the name 
		if (strcmp(sortedList->name, searchName) == 0)
		{
			success = true;
			*comparisonCount += comparisons;
			return sortedList;
		}

		//If you have passed the point in which you should have found the name
		if (strcmp(sortedList->name, searchName) >= 0)
		{
			*comparisonCount += comparisons;
			return NULL;
		}


		sortedList = sortedList->next;
	}


}




/*
 * Function: searchForNameTwice()
 * Description: This function searches for one name in two different linked lists
 *				one being a Hash Table and one being a very long linked list
 *				it uses the 'searchLinkedList()' for the searching
 * Parameters: This function takes in a char* which is the name, a struct of the long linked list, a struct of the 
 *				hash table and finally an int array of 2 comparison counts which tally the amount of comarisons 
 *				in both the hash table and long linked list
 * Return Values: This function does not return anything
 */
void searchForNameTwice(char* searchName, struct NameNode* linkedList, struct NameNode* hashTable, int comparisonCount[2])
{
	int initialLinkedListValue = 0;
	int initialHashTableValue = 0;
	struct NameNode* checker = NULL;
	char found[12] = "found";
	char found2[12] = "found";

	//Keep track of the initial comparison values, this will come in handy when
	//printing the amount of comparisons for a single name
	initialLinkedListValue = comparisonCount[0];
	initialHashTableValue = comparisonCount[1];

	//Search LONG List
	checker = searchLinkedList(searchName, linkedList, &comparisonCount[0]);
	if (checker == NULL)
	{
		strcpy(found, "NOT found");
	}
	else
	{
		strcpy(found, "found");
	}


	//Search small Hashed Bucket list
	checker = searchLinkedList(searchName, hashTable, &comparisonCount[1]);
	if (checker == NULL)
	{
		strcpy(found2, "NOT found");
	}
	else
	{
		strcpy(found2, "found");
	}

	//Print the comparisons
	printf("\t%s was %s in the linked list in %d comparisons\n", searchName, found, (comparisonCount[0] - initialLinkedListValue));
	printf("\t%s was %s in the hash table bucket in %d comparisons\n", searchName, found2, (comparisonCount[1] - initialHashTableValue));

}




/*
 * Function: freeMemory()
 * Description: This function frees all the allocated data in the structure list
 *				it saves the previous list and then flips to the next and frees the last
 *				since if you were to just free a list there would be no way to go forward to
 *				the next one.
 * Parameters: This function takes only 1 parameter which is the head of the structre itself
 * Return Values: This function returns NULL to the function
 */
void freeMemory(struct NameNode** head)
{
	NameNode* sortedList = *head;
	NameNode* freeIt = NULL;
	while (sortedList != NULL)
	{
		freeIt = sortedList;
		sortedList = sortedList->next;
		free(freeIt);
	}
}




/*
 * Function: getWordInfo()
 * Description: This function uses malloc to allocate the necessary size for the struct to then
 *				hold the specific word
 * Parameters: This function takes 2 parameters, a pointer to the head of the list and the word
 * Return Values: This function returns newHead for if something went wrong and for when it actually gets the data
 */
struct NameNode* getWordInfo(NameNode** newHead, char* name)
{
	NameNode* newBlock = NULL;
	NameNode* ptr = NULL;
	NameNode* prev = NULL;

	//Allocate the data
	newBlock = (NameNode*)malloc(sizeof(NameNode));
	if (newBlock == NULL)
	{
		printf("Out of memory\n");
		return *newHead;
	}

	//Copy the Genre and Title into the structure
	strcpy(newBlock->name, name);

	//Initilize next list
	newBlock->next = NULL;

	//Place the data in the right spots in th list
	if (*newHead == NULL)
	{
		*newHead = newBlock;
	}
	else if (strcmp((*newHead)->name, newBlock->name) >= 0)
	{
		newBlock->next = *newHead;
		*newHead = newBlock;
	}
	else
	{
		prev = *newHead;
		ptr = (*newHead)->next;

		while (ptr != NULL)
		{
			if (strcmp(ptr->name, name) >= 0)
			{
				break;
			}
			prev = ptr;
			ptr = ptr->next;
		}

		newBlock->next = ptr;
		prev->next = newBlock;
	}
	return *newHead;
}




/*
 * Function: nTakeaway()
 * Description: This function takes away the \n at the end of the strings coming in from the user
 * Parameters: This function takes only 1 parameter which is the string that you want to take the
 *				\n away from
 * Return Values: This function returns the string
 */
const char* nTakeaway(char* str)
{
	int length = 0;
	length = (unsigned)strlen(str);

	str[length - 1] = '\0';

	return str;
}