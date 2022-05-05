#include <fcntl.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>

#define MAX_LINE_LENGTH 700
#define MAX_CONTENT_LENGTH 70

#define SPLIT_CHAR '~'
#define TEXT_FILE "ProcessorCoreBenchmarkResults.txt"

typedef struct BenchmarkResult
{
	char *manufacturer, *processor, *processorCoreScore, *timeOfTest;
	char *socket, *clockSpeed, *fsb, *cache, *idleLoad, *windowsVersion;
} Result;

void QuickSortAlgorithm(Result *testResults, int left, int right)
{
    int l_hold, r_hold, spliter;
    Result pivot;

    l_hold = left;
    r_hold = right;
    pivot = testResults[left];

    while (left < right)
    {
		while ((atoi(testResults[right].processorCoreScore) <= atoi(pivot.processorCoreScore)) && (left < right))
			right--;

		if (left != right)
		{
			testResults[left] = testResults[right];
			left++;
		}

		while ((atoi(testResults[left].processorCoreScore) >= atoi(pivot.processorCoreScore)) && (left < right))
			left++;

		if (left != right)
		{
			testResults[right] = testResults[left];
			right--;
		}
    }

    testResults[left] = pivot;

    spliter = left;
    left = l_hold;
    right = r_hold;

    if (left < spliter)
		QuickSortAlgorithm(testResults, left, spliter - 1);

    if (right > spliter)
		QuickSortAlgorithm(testResults, spliter + 1, right);
}


// Descendingly sorts the test results obtained of the tests performed up to the present moment.
void SortReadData(Result *testResults, int resultsCount)
{
	QuickSortAlgorithm(testResults, 0, resultsCount - 1);
}

// Gets the current string up to the delimiter
void GetString(char *line, int *currentPosition, char spliter, char dest[])
{
	int destPosition = 0, lineLength = strlen(line);

	while ((line[*currentPosition] != spliter) && (*currentPosition < lineLength))
	{
		dest[destPosition] = line[*currentPosition];
		destPosition++;
		(*currentPosition)++;
	}

	dest[destPosition] = '\0';
	(*currentPosition)++;
}

// Creates the corresponding results structure to a given line of text 
Result BuildTestResultsEntry(char *line)
{
	char manufacturer[MAX_CONTENT_LENGTH + 1], processor[MAX_CONTENT_LENGTH + 1], 
		 processorCoreScore[MAX_CONTENT_LENGTH + 1], timeOfTest[MAX_CONTENT_LENGTH + 1];
	char socket[MAX_CONTENT_LENGTH + 1], clockSpeed[MAX_CONTENT_LENGTH + 1],
		 fsb[MAX_CONTENT_LENGTH + 1], cache[MAX_CONTENT_LENGTH + 1],
		 idleLoad[MAX_CONTENT_LENGTH + 1], windowsVersion[MAX_CONTENT_LENGTH + 1];

	int position = 0;
	
	GetString(line, &position, SPLIT_CHAR, manufacturer);
	GetString(line, &position, SPLIT_CHAR, processor);
	GetString(line, &position, SPLIT_CHAR, processorCoreScore);
	GetString(line, &position, SPLIT_CHAR, timeOfTest);
	GetString(line, &position, SPLIT_CHAR, socket);
	GetString(line, &position, SPLIT_CHAR, clockSpeed);
	GetString(line, &position, SPLIT_CHAR, fsb);
	GetString(line, &position, SPLIT_CHAR, cache);
	GetString(line, &position, SPLIT_CHAR, idleLoad);
	GetString(line, &position, SPLIT_CHAR, windowsVersion);
	
	Result currentResult;

	currentResult.manufacturer = (char*)malloc((strlen(manufacturer) + 1) * sizeof(char));
	currentResult.processor = (char*)malloc((strlen(processor) + 1) * sizeof(char));
	currentResult.processorCoreScore = (char*)malloc((strlen(processorCoreScore) + 1) * sizeof(char));
	currentResult.timeOfTest = (char*)malloc((strlen(timeOfTest) + 1) * sizeof(char));
	currentResult.socket = (char*)malloc((strlen(socket) + 1) * sizeof(char));
	currentResult.clockSpeed = (char*)malloc((strlen(clockSpeed) + 1) * sizeof(char));
	currentResult.fsb = (char*)malloc((strlen(fsb) + 1) * sizeof(char));
	currentResult.cache = (char*)malloc((strlen(cache) + 1) * sizeof(char));
	currentResult.idleLoad = (char*)malloc((strlen(idleLoad) + 1) * sizeof(char));
	currentResult.windowsVersion = (char*)malloc((strlen(windowsVersion) + 1) * sizeof(char));

	strcpy(currentResult.manufacturer, manufacturer);
	strcpy(currentResult.processor, processor);
	strcpy(currentResult.processorCoreScore, processorCoreScore);
	strcpy(currentResult.timeOfTest, timeOfTest);
	strcpy(currentResult.socket, socket);
	strcpy(currentResult.clockSpeed, clockSpeed);
	strcpy(currentResult.fsb, fsb);
	strcpy(currentResult.cache, cache);
	strcpy(currentResult.idleLoad, idleLoad);
	strcpy(currentResult.windowsVersion, windowsVersion);

	return currentResult;
}

// Returns the number of test records from a given text file.
int GetNumberOfTestRecords(char *textFile)
{
	FILE *inputFile;
	char line[MAX_LINE_LENGTH + 1];
	int fd, counter = 0;
	struct flock fl;
	
	fl.l_type = F_RDLCK;
	fl.l_whence = SEEK_SET;
	fl.l_start = 0;
	fl.l_len = 0;
	fl.l_pid = getpid();
		
	// Tests for the text file existence.
	fd = open(TEXT_FILE, O_RDONLY);
	if (fd < 0)
	    return 0;
	
	// locks the text file (blocking request)
	fcntl(fd, F_SETLKW, &fl);
	
	inputFile = fdopen(fd,"r");
	
	while (fgets(line, MAX_LINE_LENGTH + 1, inputFile) != NULL)
		counter++;

	// unlocks the text file
	fl.l_type = F_UNLCK;
	fcntl(fd, F_SETLK, &fl);

	fclose(inputFile);	
	return counter;
}	

// Reads the test results of the tests performed up to the present moment.
void ReadDataFromTextFile(char *textFile, Result *testResults, int resultsCount)
{
	char line[MAX_LINE_LENGTH + 1];
	FILE *inputFile;
	int fd, fileCursor;
	struct flock fl;
	
	fl.l_type = F_RDLCK;
	fl.l_whence = SEEK_SET;
	fl.l_start = 0;
	fl.l_len = 0;
	fl.l_pid = getpid();
		
	// Tests for the text file existence.
	fd = open(TEXT_FILE, O_RDONLY);
	
	// locks the text file (blocking request)
	fcntl(fd, F_SETLKW, &fl);
	
	inputFile = fdopen(fd,"r");
	
	// Adds all the result entries to the previously created structure
	for (fileCursor = 0; fileCursor < resultsCount; fileCursor++)
	{
		fgets(line, MAX_LINE_LENGTH + 1, inputFile);
		testResults[fileCursor] = BuildTestResultsEntry(line);
	}
	
	// unlocks the text file
	fl.l_type = F_UNLCK;
	fcntl(fd, F_SETLK, &fl);
	
	fclose(inputFile);
}

// Generates the visual formatting (CSS) to be embedded into the HTML document 
void GenerateCSSStyles()
{
	printf("<style type=\"text/css\">\n");
	printf("body {background-color: #99FF99;font-family: Arial, Helvetica, sans-serif;font-size: small;}\n");
	printf("h1 {padding-top: 5px;text-align: center;color: #A00000;}\n");
	printf("table {width: 99%%;margin-top: 5px;margin-bottom: 5px;margin-left: auto;margin-right: auto;");
	printf("border: 2px solid #66CCFF;table-layout: auto;background-color: #FFFFCC;}\n");
	printf("tr {color: #0066FF;text-align: center}\n");
	printf("th {background-color: #FFFF66;color: #FF2200;border: 1px solid #3399FF;}\n");
	printf("td {border: 1px solid #66CCFF;font-family: \"Times New Roman\", Times, serif;}\n");
	printf("#newResultRow {background-color: #FF9966;color: #003366;}\n");
	printf("</style>\n");
}

// Generates the table content of the HTML document
void GenerateTableContent(Result *testResults, int resultsCount, int highlightedColumn)
{
	int rankCounter = 0, currentScore = 0;
	int processorCoreScoreChanged = 0;
	int i;
	
	printf("<table cellpadding=\"4\"><tr>\n");
	printf("<th rowspan=\"2\">Rank</th>\n");
	printf("<th rowspan=\"2\">Manufacturer</th>\n");
	printf("<th rowspan=\"2\">Processor</th>\n");
	printf("<th rowspan=\"2\">Processor Core Score</th>\n");
	printf("<th rowspan=\"2\">Time Of Test (Universal)</th>\n");
	printf("<th colspan=\"6\">Additional Information</th></tr>\n");
	printf("<tr><th>Socket</th><th>Clock Speed</th><th>FSB Speed</th>");
	printf("<th>L2 Cache Size</th><th>Idle Load</th>");
	printf("<th>Windows Version</th></tr>\n");

	if (testResults != NULL)
		for (i = 0; i < resultsCount; i++)
		{
			int processorCoreScore = atoi(testResults[i].processorCoreScore);

			if (processorCoreScore != currentScore)
			{
				rankCounter++;
				currentScore = processorCoreScore;
				processorCoreScoreChanged = 1;
			}

			if (i == highlightedColumn)
				printf("<tr id=\"newResultRow\">");
			else
				printf("<tr>");

			if (processorCoreScoreChanged == 1)
			{
				printf("<td>%d.</td><td>%s</td><td>%s</td><td>%d</td><td>%s</td>\n",
				       rankCounter, testResults[i].manufacturer, testResults[i].processor, processorCoreScore,
					   testResults[i].timeOfTest);
				processorCoreScoreChanged = 0;
			}
			else
				printf("<td>&nbsp;</td><td>%s</td><td>%s</td><td>%d</td><td>%s</td>\n",
				       testResults[i].manufacturer, testResults[i].processor, processorCoreScore,
					   testResults[i].timeOfTest);
			
			printf("<td>%s</td><td>%s</td><td>%s</td><td>%s</td><td>%s</td><td>%s</td></tr>\n",
				   testResults[i].socket, testResults[i].clockSpeed, testResults[i].fsb,
				   testResults[i].cache, testResults[i].idleLoad, testResults[i].windowsVersion);
		}

	printf("</table>");
}

// Generates the HTML tabular output.
void GenerateHTMLOutput(Result *testResults, int resultsCount, int highlightedColumn)
{
	printf("Content-Type:text/html;charset=iso-8859-1\n\n");
	printf("<title>Processor Core Benchmark Results</title>\n");

	GenerateCSSStyles();

	printf("<h1>Processor Core Benchmark Results</h1>\n");
	GenerateTableContent(testResults, resultsCount, highlightedColumn);
}

// Removes the special %20 (space) character from a GET request variable
void RemoveSpecialCharacters(char *source, char *dest)
{
	int cursorSource = 0, cursorDest = 0, sourceLength = strlen(source);

	while (cursorSource < sourceLength)
	{
		if ((cursorSource + 2 < sourceLength) &&
			(((source[cursorSource] == '%') && (source[cursorSource + 1] == '2')) &&
			  (source[cursorSource + 2] == '0')))
			{
				dest[cursorDest] = ' ';
				cursorSource += 3;
				cursorDest++;
			}

		else
		{
			dest[cursorDest] = source[cursorSource];
			cursorSource++;
			cursorDest++;
		}
	}

	dest[cursorDest] = '\0';
}

// Obtains the column that is to be highlighted, corresponding to the GET request
int ObtainHighlightedColumn(Result *testResults, int resultsCount, char *getRequest)
{
	char manufacturerTemp[MAX_CONTENT_LENGTH + 1], processorTemp[MAX_CONTENT_LENGTH + 1], 
		 processorCoreScoreTemp[MAX_CONTENT_LENGTH + 1], timeOfTestTemp[MAX_CONTENT_LENGTH + 1],
		 manufacturer[MAX_CONTENT_LENGTH + 1], processor[MAX_CONTENT_LENGTH + 1], 
		 processorCoreScore[MAX_CONTENT_LENGTH + 1], timeOfTest[MAX_CONTENT_LENGTH + 1],
		 temp[MAX_CONTENT_LENGTH + 1];

	int i, position = 0;

	GetString(getRequest, &position, '=', temp);
	GetString(getRequest, &position, '&', manufacturerTemp);
	GetString(getRequest, &position, '=', temp);
	GetString(getRequest, &position, '&', processorTemp);
	GetString(getRequest, &position, '=', temp);
	GetString(getRequest, &position, '&', processorCoreScoreTemp);
	GetString(getRequest, &position, '=', temp);
	GetString(getRequest, &position, '&', timeOfTestTemp);

	RemoveSpecialCharacters(manufacturerTemp, manufacturer);
	RemoveSpecialCharacters(processorTemp, processor);
	RemoveSpecialCharacters(processorCoreScoreTemp, processorCoreScore);
	RemoveSpecialCharacters(timeOfTestTemp, timeOfTest);

	for (i = 0; i < resultsCount; i++)
		if (((strcmp(manufacturer, testResults[i].manufacturer) == 0) &&
			 (strcmp(processor, testResults[i].processor) == 0)) &&
		    ((strcmp(processorCoreScore, testResults[i].processorCoreScore) == 0) &&
			 (strcmp(timeOfTest, testResults[i].timeOfTest) == 0)))
				return i;

	return -1;
}

// Gets the size of the given file in bytes 
long GetFileSize(FILE *file)
{
    long currentPosition, fileSize;
    
    currentPosition = ftell(file);
    fseek(file, 0, SEEK_END);
    fileSize = ftell(file);
    fseek(file, currentPosition, SEEK_SET);
    
    return fileSize;
}

// Cleans up the heap.
void PerformMemoryCleanup(Result *testResults, int resultsCount, char *postRequest)
{
	int i;
	
	if (testResults != NULL)
	{
		for (i = 0; i < resultsCount; i++)
		{
			free(testResults[i].manufacturer);
			free(testResults[i].processor);
			free(testResults[i].processorCoreScore);
			free(testResults[i].timeOfTest);
			free(testResults[i].socket);
			free(testResults[i].clockSpeed);
			free(testResults[i].fsb);
			free(testResults[i].cache);
			free(testResults[i].idleLoad);
			free(testResults[i].windowsVersion);
		}

		free(testResults);
	}

	if (postRequest != NULL)
		free(postRequest);
}

int main()
{
	char *getRequest = NULL, *postRequest = NULL, *postRequestLengthString = NULL;
	int postRequestLength, resultsCount = 0, highlightedColumn = -1;
	Result *testResults = NULL;
	
	if ((postRequestLengthString = getenv("CONTENT_LENGTH")) != NULL)
	{
		FILE *outputFile;
		int fd;
		struct flock fl;
		
		postRequestLength = atoi(postRequestLengthString);
		postRequest = (char*)malloc((postRequestLength + 1) * sizeof(char));
		fgets(postRequest, postRequestLength + 1, stdin);
		
		fl.l_type = F_WRLCK;
		fl.l_whence = SEEK_SET;
		fl.l_start = 0;
		fl.l_len = 0;
		fl.l_pid = getpid();
		
		fd = open(TEXT_FILE, O_WRONLY | O_APPEND);
		
		// locks the text file (blocking request)
		fcntl(fd, F_SETLKW, &fl);
			
		outputFile = fdopen(fd, "a");
		
		if (GetFileSize(outputFile) != 0)
		    fprintf(outputFile, "\n");
		fprintf(outputFile, "%s", postRequest);
		
		// unlocks the text file
		fl.l_type = F_UNLCK;
		fcntl(fd, F_SETLK, &fl);
		
		fclose(outputFile);

		printf("Content-Type:text/plain\n\n");
		printf("%s", postRequest);
	}

	else
	{
		resultsCount = GetNumberOfTestRecords(TEXT_FILE);
		if (resultsCount > 0)
		{
			testResults = (Result*)(malloc(resultsCount * sizeof(Result)));
			ReadDataFromTextFile(TEXT_FILE, testResults, resultsCount);
			SortReadData(testResults, resultsCount);
		}

		if ((getRequest = getenv("QUERY_STRING")) != NULL)
			highlightedColumn = ObtainHighlightedColumn(testResults, resultsCount, getRequest);

		GenerateHTMLOutput(testResults, resultsCount, highlightedColumn);
	}

	PerformMemoryCleanup(testResults, resultsCount, postRequest);

	return 0;
}
