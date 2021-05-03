
// DO NOT FORGET TO ADD A REFERENCE OF YOUR GUIDE FILE IN THE INDEX.HTML FILE
$(function() {
    registerGuide({ 
        name: 'Video Progress of the project', // THE NAME OF THE PAGE
        summary: 'Recording of each milestone reached in the project', // A SUMMARY DISPLAYED IN THE GUIDES PAGES
        content: `
            <h1 class='page-heading'>Video Progress</h1>

            <section>
               <label for="pwd">Password:</label>
               <button id="pwd" onclick="password()">Show video links</button>
            </section>

            <section>
            <ul id="secrets">

            </ul>

        </section>
        `
    });
});