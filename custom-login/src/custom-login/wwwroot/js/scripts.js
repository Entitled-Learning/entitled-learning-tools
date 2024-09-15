// Select the div with the class 'emailVerificationControl_success_message'
const successMessageDiv = document.getElementById('emailVerificationControl_success_message');
const errorMessageDiv = document.getElementById('emailVerificationControl_error_message');

// Set up a MutationObserver to watch emailVerificationControl_success_message
if (successMessageDiv && errorMessageDiv) {
    const observer = new MutationObserver((mutationsList, observer) => {
        mutationsList.forEach(mutation => {
            if (mutation.type === 'attributes' && mutation.attributeName === 'style') {
                // Check if display is no longer 'none'
                if ((window.getComputedStyle(successMessageDiv).display !== 'none' && successMessageDiv.innerText.trim() === 'Verification code has been sent to your inbox. Please copy it to the input box below.') 
                    || (window.getComputedStyle(errorMessageDiv).display !== 'none' && errorMessageDiv.innerText.trim() === 'The verification has failed, please try again.')) {
                    // Unhide the target divs when the display is changed from 'none'
                    verifyCodeState();
                }

                if (window.getComputedStyle(successMessageDiv).display !== 'none' && successMessageDiv.innerText.trim() === 'E-mail address verified. You can now continue.') {
                    // Unhide the target divs when the display is changed from 'none'
                    enterUserInfoState();
                }
            }
        });
    });

    // Start observing the target div for changes in attributes, specifically 'style'
    observer.observe(successMessageDiv, { attributes: true, attributeFilter: ['style'] });
    observer.observe(errorMessageDiv, { attributes: true, attributeFilter: ['style'] });
}


function verifyCodeState() {
    console.log('verifyCodeState() called');

    const sendInitialCode = document.getElementById('emailVerificationControl_but_send_code'); 
    sendInitialCode.style.setProperty('display', 'none', 'important');

    const divClasses = ['.sendNewCode', '.verifyCode', '.changeClaims']; 

    divClasses.forEach(divClass => {
        const div = document.querySelector(divClass); 
        if (div) {
            div.style.setProperty('display', 'inline', 'important');
        }
    });
}

function updateDisplayName() {
    // Get the value of the email input field
    const emailInput = document.getElementById('email');
    const displayNameInput = document.getElementById('displayName');

    // Set the value of the displayname input to the value of the email input
    displayNameInput.value = emailInput.value;
}

function enterUserInfoState() {
    console.log('enterUserInfoState() called');

    try {
        updateDisplayName();
    } catch (error) {
        console.error(error);
    }

    const sendInitialCode = document.getElementById('emailVerificationControl_but_send_code'); 
    sendInitialCode.style.setProperty('display', 'none', 'important');


    // Hide the divs that are not needed
    const classesToHide = ['.sendNewCode', '.verifyCode', '.changeClaims']; 

    classesToHide.forEach(divClass => {
        const div = document.querySelector(divClass); 
        if (div) {
            div.style.setProperty('display', 'none', 'important');
        }
    });

    // Show the divs that are needed
    const classesToShow = ['.Password.newPassword_li', '.Password.reenterPassword_li', '.TextBox.givenName_li', '.TextBox.surname_li ']; 

    classesToShow.forEach(divClass => {
        const div = document.querySelector(divClass); 
        if (div) {
            div.style.setProperty('display', 'inline', 'important');
        }
    });

    // Show Id that is needed
    const idToShow = document.getElementById('continue');
    idToShow.style.setProperty('display', 'inline', 'important');
}

function formatHelpLinks() {
    // Select all div elements with the class 'helpLink' and 'tiny'
    const helpLinks = document.querySelectorAll('a.helpLink.tiny');
        
    // Loop through each element and change the inner HTML to a Font Awesome question mark icon
    helpLinks.forEach(div => {
        div.innerHTML = '<i class="fas fa-question-circle"></i>';
    });

    // Loop through each element and check if the 'data-help' attribute is present
    helpLinks.forEach(anchor => {
        if (!anchor.hasAttribute('data-help')) {
            anchor.classList.add('hidden'); // Add 'hidden' class if 'data-help' is not present
        }
    });
}


formatHelpLinks();