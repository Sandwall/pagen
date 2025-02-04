package main

import (
	"fmt"
	"os"
	"path/filepath"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

// We want to make sure that the program is
func get_rootdir() string {
	cwd, err := os.Getwd()
	check(err)

	dir, _ := filepath.Split(cwd)
	entries, err := os.ReadDir(dir)
	check(err)

	var validFolders int

	for entry := range entries {
		switch entries[entry].Name() {
		case "pages":
			validFolders++
		case "site":
			validFolders++
		}
	}

	if validFolders >= 2 {
		return dir
	} else {
		return ""
	}
}

type Post struct {
	title   string
	content string
}

type HtmlTag struct {
	tag  string
	data string
}

func main() {
	fmt.Println(os.Args)

	rootdir := get_rootdir()
	if rootdir == "" {
		fmt.Println("Make sure this program is running inside of the pagen folder...")
		return
	}

	// generate main page, export to site/index.html

	// generate child pages, give it a tree structure

}
