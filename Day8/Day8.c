#include <stdio.h>
#include <string.h>
#include <stdlib.h>

typedef struct Node_T // node
{
    char val[4];
    char left[4];
    char right[4];
} Node;

typedef struct List_T // list of nodes
{
    Node* node;
    struct List_T* next;
} List;

Node* FindNode(List start, char* val) // find node in list
{
    List* iter = &start;
    while (iter != NULL)
    {
        if (strcmp(iter->node->val, val) == 0) return iter->node;
        iter = iter->next;
    }

    return NULL;
}

unsigned long GCD(unsigned long a, unsigned long b)
{
    return a == 0 ? b : GCD(b % a, a);
}

unsigned long LCM(unsigned long a, unsigned long b)
{
    return (a / GCD(a, b)) * b;
}

int main(void)
{
    FILE *file = fopen("input.txt", "r");
    
    // read first line of file to get instructions
    char directions[300];
    fgets(directions, sizeof(directions), file);
    int dirsLen = strlen(directions) - 2;

    // read file and transform into list
    List start;
    List* next = &start;
    char currentline[17];
    while (fgets(currentline, sizeof(currentline), file) != NULL)
    {
        if (strlen(currentline) != 16) continue;
        
        Node* newNode = malloc(sizeof(Node));
        strncpy(newNode->val, currentline, 3);
        strncpy(newNode->left, currentline + 7, 3);
        strncpy(newNode->right, currentline + 12, 3);

        if (start.node != NULL) next = next->next = malloc(sizeof(List));
        next->node = newNode;
    }
    fclose(file);

{
    // part 1
    unsigned long steps = 0;
    Node* curr = FindNode(start, "AAA");
    if (curr == NULL) return 0;

    while (strcmp(curr->val, "ZZZ") != 0)
    {
        if (directions[steps % dirsLen] == 'L') curr = FindNode(start, curr->left);
        else curr = FindNode(start, curr->right);
        steps++;
    }

    printf("Part 1 is %ld\n", steps);
}

{
    // part 2
    unsigned long answer = 1;
    List* iter = &start;
    while (iter != NULL)
    {
        if (strncmp(iter->node->val + 3 - 1, "A", 1) == 0) // ends with A
        {
            unsigned long steps = 0;
            Node* curr = iter->node;
            
            while (strncmp(curr->val + 3 - 1, "Z", 1) != 0) // ends with Z
            {
                if (directions[steps % dirsLen] == 'L') curr = FindNode(start, curr->left);
                else curr = FindNode(start, curr->right);
                steps++;
            }

            answer = LCM(answer, steps);
        }
        
        iter = iter->next;
    }

    printf("Part 2 is %ld\n", answer);
}

    return 0;
}