# How to merge with git bash and VSCode
*(This is what I did and what worked best for me)*

Case: You have committed all your changes to your local branch and now want to merge this branch to the developER branch using git bash

## 1. Merge developer branch into your local branch
(you have to be inside your local branch)

*git merge origin/developer*

- Case 1: No merge conflicts -> proceed to 4.
- Case 2: Merge conflict -> proceed to 2.

## 2. Resolving merge conflicts using VSCode
if you receive a merge conflict looking something like this:

Auto-merging ITProject/ProjectSettings/EditorBuildSettings.asset
CONFLICT (content): Merge conflict in ITProject/ProjectSettings/EditorBuildSettings.asset

- open conflicted file in VSCode
- you will see the conflict marked like this:
  <<<<<<< HEAD:EditorBuildSettings.asset
  ...
- choose to either accept remote version or your local version (options are listed above the conflict)
- save the file

## 3. Commit your modified local branch
- add changed files: *(git add .)*
- check if conflicts are resolved: *(git status)* 
-> it should say something like: All conflicts fixed but you are still merging.
- commit changes to local branch: *(git commit -m "...")*

## 4. Merge local branch into developer branch
- switch to developer branch: *(git checkout developer)*
- check to see if you have uncommitted changes: *(git status)* and add them if you do: *(git add .)*
- merge local branch into developer: *(git merge <myLocalBranch>)*
- commit the changes: *(git commit -m "...")*
