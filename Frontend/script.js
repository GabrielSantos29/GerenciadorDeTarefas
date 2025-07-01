
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

    //formulário para adicionar tarefas
    const btnAdicionar = document.getElementById("btnAdicionar");
    const modal = document.getElementById("formAdicionar");
    const inputNome = document.getElementById("inputNomeTarefa");
    const btnConfirmar = document.getElementById("btnConfirmarAdicionar");
    const btnCancelar = document.getElementById("btnCancelarAdicionar");

    btnAdicionar.addEventListener("click", () => {
        modal.classList.remove("oculto");
        inputNome.value = "";
        inputNome.focus();
    });

    btnCancelar.addEventListener("click", () => {
        modal.classList.add("oculto");
    });

    btnConfirmar.addEventListener("click", async () => {
        const nome = inputNome.value.trim();
        if (nome === "") {
            alert("Digite o nome da tarefa.");
            return;
        }

        const novaTarefa = {
            nome: nome,
            concluida: false
        };

        try {
            await fetch("http://localhost:5006/api/tarefas", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(novaTarefa)
            });

            modal.classList.add("oculto");
            carregarTarefas(); // Atualiza a lista de tarefas
        } catch (erro) {
            console.error("Erro ao adicionar tarefa:", erro);
        }
    });

