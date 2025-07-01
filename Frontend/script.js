
    const botao = document.getElementById('btnMenu');
    const menu = document.getElementById('Menu');

    botao.addEventListener('click', () => {
        menu.classList.toggle('visivel');
    });

    const box = document.getElementById("Box");

    async function carregarTarefas() {
        try {
            const resposta = await fetch("http://localhost:5006/api/tarefas");
            const tarefas = await resposta.json();

            box.innerHTML = ""; // Limpa o conteúdo

            tarefas.forEach(tarefa => {
                const div = document.createElement("div");
                div.className = "tarefa";
                div.innerHTML = `
                    <p><strong>${tarefa.nome}</strong></p>
                    <p>Status: ${tarefa.concluida ? "✅ Concluída" : "🕓 Pendente"}</p>
                    <hr>
                `;
                box.appendChild(div);
            });

        } catch (erro) {
            console.error("Erro ao carregar tarefas:", erro);
            box.innerHTML = "<p>Erro ao carregar tarefas</p>";
        }
    }

    // Quando a página carrega
    document.addEventListener("DOMContentLoaded", carregarTarefas);
