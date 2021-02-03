
// DO NOT FORGET TO ADD A REFERENCE OF YOUR GUIDE FILE IN THE INDEX.HTML FILE
$(function() {
    registerGuide({ 
        name: 'Secrets', // THE NAME OF THE PAGE
        summary: 'Play the game to discover the secret word', // A SUMMARY DISPLAYED IN THE GUIDES PAGES
        content: `
            <h1 class='page-heading'>Secrets</h1>

            <section>
               <label for="pwd">Password:</label>
               <input type="password" id="pwd" name="pwd" onchange="password()">
            </section>

            <section>
             <span class=‘section-heading'>Find the nickname of one of the murderers in the game to unlock this content (the nickname need to be wrote in lower case)</span>
<ul id="secrets">

</ul>

        </section>
        `
    });
});