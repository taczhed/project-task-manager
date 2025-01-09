document.addEventListener("DOMContentLoaded", () => {
    const updateTaskStats = () => {
        fetch('/api/taskstats')
            .then(response => response.json())
            .then(data => {
                document.querySelector('.task-stats').innerHTML = `
                    <h2>Task Statistics</h2>
                    <div>
                        <p>Completed Tasks: ${data.CompletedPercentage}%</p>
                        <p>In Progress Tasks: ${data.InProgressPercentage}%</p>
                        <p>Overdue Tasks: ${data.OverduePercentage}%</p>
                    </div>`;
            });
    };

    
    const solutionForm = document.querySelector('form[asp-action="AddSolution"]');
    if (solutionForm) {
        solutionForm.addEventListener("submit", (event) => {
            event.preventDefault();

            const formData = new FormData(solutionForm);
            fetch(solutionForm.action, {
                method: "POST",
                body: formData
            })
                .then(response => {
                    if (response.ok) {
                        updateTaskStats();
                        location.reload();
                    } else {
                        alert("Error saving the solution.");
                    }
                })
                .catch(err => console.error("Fetch error: ", err));
        });
    }
});
